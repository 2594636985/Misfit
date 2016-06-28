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
using Misfit.IO;

namespace Misfit.Modulation
{
    /// <summary>
    /// 框架调用类
    /// </summary>
    public class MisfitWeaver
    {
        private ModulationWorker _modulationWorker;
        public string ConfigurationString { private set; get; }


        public event Action<Exception> OnMisfitException;

        public MisfitWeaver()
            : this("Misfit.xml")
        {

        }

        public MisfitWeaver(string configurationString)
        {
            this.ConfigurationString = configurationString;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initailize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            string configurationDirectory = string.Empty;
            if (Path.IsPathRooted(this.ConfigurationString))
            {
                configurationDirectory = this.ConfigurationString;
            }
            else
            {
                configurationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.ConfigurationString);
            }
            if (!File.Exists(configurationDirectory))
                throw new FileNotFoundException("对应的配置文件Misfit.xml不存在");

            MisfitDocument misfitDocument = new MisfitDocument();
            misfitDocument.Load(configurationDirectory);

            MisfitNode misfitNode = misfitDocument.MisfitNode;

            if (misfitNode != null)
            {
                List<Module> modules = new List<Module>();
                Dictionary<string, string> misfitArguments = new Dictionary<string, string>();

                foreach (ArgumentNode argumentNode in misfitNode.ArgumentNodes)
                {
                    misfitArguments.Add(argumentNode.Name, argumentNode.Value);
                }

                foreach (ModuleNode pluginNode in misfitNode.ModuleNodes)
                {
                    Module module = new Module();
                    module.Description = pluginNode.Description;
                    module.Name = pluginNode.Name;
                    module.Location = pluginNode.Location;

                    foreach (ArgumentNode connectionStringNode in pluginNode.ArgumentNodes)
                    {
                        module.Arguments.Add(connectionStringNode.Name, connectionStringNode.Value);
                    }

                    modules.Add(module);
                }

                ModulationWorkerContext modulationWorkerContext = new ModulationWorkerContext()
                {
                    Arguments = misfitArguments,
                    Modules = modules
                };

                _modulationWorker = new ModulationWorker(modulationWorkerContext);
                _modulationWorker.OnModulationException += ModulationWorker_OnModulationException;
                _modulationWorker.Initialize();
            }

            if (_modulationWorker == null)
                throw new InvalidOperationException("模块功能初始化失败 原因：配置文件可能存在问题");

            _modulationWorker.Start();

        }

        private void ModulationWorker_OnModulationException(IModulationWorker mWorker, ModulationException mex)
        {
            if (this.OnMisfitException != null)
                this.OnMisfitException(mex);
        }


        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            if (_modulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            return _modulationWorker.GetService(assemblyCatalogName, typeName);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            if (_modulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            return _modulationWorker.GetService<TInterface>(assemblyCatalogName);
        }



        public void Dispose()
        {
            if (_modulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            _modulationWorker.Stop();
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
    }
}
