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
using Misfit.Core;

namespace Misfit
{
    /// <summary>
    /// 框架调用类
    /// </summary>
    public class MisfitWeaver
    {
        public static ModuleDomainRepository ModuleDomainRepository = new ModuleDomainRepository();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initailize()
        {
            AppDomain.CurrentDomain.ResourceResolve += AssemblyResolve;

            IO.MisfitConfiguration misiftConfiguration = new IO.MisfitConfiguration();
            misiftConfiguration.Initialize();

            if (misiftConfiguration.MisfitDescriptor == null)
                throw new NullReferenceException("配置初始化失败，找看一下文件是否存在");

            MisfitDescriptor misfitDescriptor = misiftConfiguration.MisfitDescriptor;

            foreach (ModuleDescriptor moduleDescriptor in misfitDescriptor.PluginDescriptors)
            {

                ModuleDomainContext moduleDomainContext = new ModuleDomainContext();
                moduleDomainContext.AssemlbyLocation = moduleDescriptor.Location;
                moduleDomainContext.ModuleDomainRepository = ModuleDomainRepository;
                moduleDomainContext.MisfitConnectionString = misfitDescriptor.MisfitConnectionString;

                foreach (string key in moduleDescriptor.ConnectionStrings.Keys)
                {
                    moduleDomainContext.ConnectionStrings.Add(key, moduleDescriptor.ConnectionStrings[key]);
                }

                ModuleDomain moduleDomain = new ModuleDomain(moduleDomainContext);
                moduleDomain.Install();

                ModuleDomainRepository.AddModulDomain(moduleDomain);

            }
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static object GetService(string assemblyCatalogName, string typeName)
        {
            return ModuleDomainRepository.GetService(assemblyCatalogName, typeName);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public static TInterface GetService<TInterface>(string assemblyCatalogName) where TInterface : AddIn.Serices.IMisfitService
        {
            return ModuleDomainRepository.GetService<TInterface>(assemblyCatalogName);
        }



        public static void Dispose()
        {
            ModuleDomainRepository.DestroyClose();
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
