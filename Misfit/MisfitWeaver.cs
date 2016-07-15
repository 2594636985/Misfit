using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Security.Policy;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Misfit.Configuration;
using Misfit.Configuration.Elements;
using Misfit.Domain;

namespace Misfit
{
    /// <summary>
    /// 框架调用类
    /// </summary>
    public class MisfitWeaver
    {
        private PluginDomainFramework _pluginDomainFramework;
        public MisfitConfiguration MisfitConfiguration { private set; get; }

        public event Action<Exception> OnMisfitException;

        #region 公有方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.Initialize(null);
        }

        /// <summary>
        /// 根据配置文件来初始化
        /// </summary>
        public void Initialize(string configurationFile)
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            if (string.IsNullOrWhiteSpace(configurationFile))
                this.MisfitConfiguration = new MisfitConfiguration();
            else
                this.MisfitConfiguration = new MisfitConfiguration(configurationFile);

            string addInsRoot = this.MisfitConfiguration.SectionHandler.AddInsRoot.Value;
            if (!string.IsNullOrWhiteSpace(addInsRoot))
                AppDomain.CurrentDomain.SetData("AddInsRoot", addInsRoot);

            List<Plugin> plugins = new List<Plugin>();
            Dictionary<string, string> variables = new Dictionary<string, string>();

            foreach (VariableElement variableElement in this.MisfitConfiguration.SectionHandler.Variables)
            {
                variables.Add(variableElement.Name, variableElement.Value);
            }

            foreach (PluginElement moduleElement in this.MisfitConfiguration.SectionHandler.Modules)
            {
                Plugin module = new Plugin();
                module.Description = moduleElement.Description;
                module.Name = moduleElement.Name;
                module.AssemblyLocation = moduleElement.Location;
                module.IsDebug = moduleElement.Debug && this.MisfitConfiguration.SectionHandler.Debug;
                module.Uri = moduleElement.Uri.Value;

                foreach (ParameterElement parameterElement in moduleElement.Parameters)
                {
                    module.Parameters.Add(parameterElement.Key, parameterElement.Value);
                }

                plugins.Add(module);
            }

            this._pluginDomainFramework = new PluginDomainFramework(plugins, variables, addInsRoot);
            this._pluginDomainFramework.OnModulationException += ModulationWorker_OnModulationException;
            this._pluginDomainFramework.Initialize();

            if (this._pluginDomainFramework == null)
                throw new InvalidOperationException("插件功能初始化失败 原因：配置文件可能存在问题");

            this._pluginDomainFramework.Start();
        }

        /// <summary>
        /// 获得相关插件域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            if (this._pluginDomainFramework == null)
                throw new InvalidOperationException("插件功能没有初始化");

            return this._pluginDomainFramework.GetService(assemblyCatalogName, typeName);
        }

        /// <summary>
        /// 获得相关插件域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            if (this._pluginDomainFramework == null)
                throw new InvalidOperationException("插件功能没有初始化");

            return this._pluginDomainFramework.GetService<TInterface>(assemblyCatalogName);
        }



        public void Dispose()
        {
            if (this._pluginDomainFramework == null)
                throw new InvalidOperationException("插件功能没有初始化");

            this._pluginDomainFramework.Stop();
        }
        #endregion
        #region 私有方法

        private void ModulationWorker_OnModulationException(IPluginDomainFramework mWorker, PluginDomainException mex)
        {
            if (this.OnMisfitException != null)
                this.OnMisfitException(mex);
        }

        /// <summary>
        /// 如果加载失败之后，去搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static System.Reflection.Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            System.Reflection.AssemblyName name = new System.Reflection.AssemblyName(args.Name);
            string assemblyFile = SearchAssembly(name.Name);
            return System.Reflection.Assembly.LoadFrom(assemblyFile);
        }

        /// <summary>
        /// 搜索对应的程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static string SearchAssembly(string assemblyName)
        {
            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            string addInsRoot = Path.Combine(appRoot, Convert.ToString(AppDomain.CurrentDomain.GetData("AddInsRoot")));
            {
                string[] files = Directory.GetFiles(appRoot,
                    assemblyName + ".dll", SearchOption.TopDirectoryOnly);

                if (files != null && files.Length > 0)
                {
                    return files[0];
                }
            }

            {
                string[] files = Directory.GetFiles(addInsRoot,
                    assemblyName + ".dll", SearchOption.TopDirectoryOnly);

                if (files != null && files.Length > 0)
                {
                    return files[0];
                }
            }

            return string.Empty;
        }
        #endregion
    }
}
