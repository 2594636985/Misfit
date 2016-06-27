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
        private static ModulationWorker ModulationWorker;

        public static Action<>

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initailize()
        {

            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            string configurationString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Misfit.xml");

            if (!File.Exists(configurationString))
                throw new FileNotFoundException("对应的配置文件Misfit.xml不存在");

            MisfitDocument pluginDocument = new MisfitDocument();
            pluginDocument.Load(configurationString);

            MisfitNode misfitNode = pluginDocument.MisfitNode;
            if (misfitNode != null)
            {
                List<Module> modules = new List<Module>();
                Dictionary<string, string> misfitArguments = new Dictionary<string, string>();

                foreach (ArgumentNode argumentNode in misfitNode.ArgumentNodes)
                {
                    misfitArguments.Add(argumentNode.Name, argumentNode.Value);
                }

                foreach (ModuleNode pluginNode in misfitNode.PluginNodes)
                {
                    Module module = new Module();
                    module.Description = pluginNode.Description;
                    module.Name = pluginNode.Name;
                    module.Location = pluginNode.Location;

                    foreach (ArgumentNode connectionStringNode in pluginNode.ConnectionStringNodes)
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

                ModulationWorker = new ModulationWorker(modulationWorkerContext);
                ModulationWorker.Initialize();
            }

            if (ModulationWorker == null)
                throw new InvalidOperationException("模块功能初始化失败 原因：配置文件可能存在问题");

            ModulationWorker.Start();

        }


        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static object GetService(string assemblyCatalogName, string typeName)
        {
            if (ModulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            return ModulationWorker.GetService(assemblyCatalogName, typeName);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public static TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            if (ModulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            return ModulationWorker.GetService<TInterface>(assemblyCatalogName);
        }



        public static void Dispose()
        {
            if (ModulationWorker == null)
                throw new InvalidOperationException("模块功能没有初始化");

            ModulationWorker.Stop();
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
