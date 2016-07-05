using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;
using System.Security.Policy;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Misfit.Configuration;
using Misfit.Configuration.Elements;
using Misfit.Modulation.Tracking;

namespace Misfit.Modulation
{
    /// <summary>
    /// 框架调用类
    /// </summary>
    public class MisfitWeaver
    {
        private ModulationWorker _modulationWorker;

        public event Action<Exception> OnMisfitException;

        #region 公有方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initailize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            MisfitConfiguration misfitConfiguration = new MisfitConfiguration();

            this.DoInitailize(misfitConfiguration.SectionHandler);
        }

        /// <summary>
        /// 根据配置文件来初始化
        /// </summary>
        public void Initailize(string configurationFile)
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            MisfitConfiguration misfitConfiguration = new MisfitConfiguration(MisfitSectionHandler.DefaultSectionName, configurationFile);

            this.DoInitailize(misfitConfiguration.SectionHandler);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            if (this._modulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            return this._modulationWorker.GetService(assemblyCatalogName, typeName);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            if (this._modulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            return this._modulationWorker.GetService<TInterface>(assemblyCatalogName);
        }



        public void Dispose()
        {
            if (this._modulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            this._modulationWorker.Stop();
        }
        #endregion
        #region 私有方法
        private void DoInitailize(MisfitSectionHandler sectionHandler)
        {
            List<Module> modules = new List<Module>();
            Dictionary<string, string> variables = new Dictionary<string, string>();

            foreach (VariableElement variableElement in sectionHandler.Variables)
            {
                variables.Add(variableElement.Name, variableElement.Value);
            }

            foreach (ModuleElement moduleElement in sectionHandler.Modules)
            {
                Module module = new Module();
                module.Description = moduleElement.Description;
                module.Name = moduleElement.Name;
                module.Location = moduleElement.Location;
                module.IsDebug = moduleElement.Debug && sectionHandler.Debug;
                module.Uri = moduleElement.Uri.Value;
                module.TrackerTarget = (TrackerTarget)Enum.Parse(typeof(TrackerTarget), moduleElement.Tracker.Value, true);

                foreach (ParameterElement parameterElement in moduleElement.Parameters)
                {
                    module.Arguments.Add(parameterElement.Key, parameterElement.Value);
                }

                modules.Add(module);
            }

            ModulationWorkerContext modulationWorkerContext = new ModulationWorkerContext();

            modulationWorkerContext.Variables = variables;
            modulationWorkerContext.Modules = modules;
            modulationWorkerContext.AddInsRoot = sectionHandler.AddInRoot.Value;

            this._modulationWorker = new ModulationWorker(modulationWorkerContext);
            this._modulationWorker.OnModulationException += ModulationWorker_OnModulationException;
            this._modulationWorker.Initialize();

            if (this._modulationWorker == null)
                throw new InvalidOperationException("模块功能初始化失败 原因：配置文件可能存在问题");

            this._modulationWorker.Start();
        }




        private void ModulationWorker_OnModulationException(IModulationWorker mWorker, ModulationException mex)
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
        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName name = new AssemblyName(args.Name);
            string assemblyFile = SearchAssembly(name.Name);
            return Assembly.LoadFrom(assemblyFile);
        }

        /// <summary>
        /// 搜索对应的程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static string SearchAssembly(string assemblyName)
        {
            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            string addInsRoot = Path.Combine(appRoot, "AddIns");
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
