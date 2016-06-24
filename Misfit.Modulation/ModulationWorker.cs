using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块工作者
    /// </summary>
    public class ModulationWorker : IModulationWorker
    {
        /// <summary>
        /// 模块域的库存
        /// </summary>
        public ModuleDomainRepository ModuleDomainRepository { private set; get; }

        /// <summary>
        /// 模块工作者的上下文
        /// </summary>
        public ModulationWorkerContext ModulationWorkerContext { private set; get; }

        public ModulationWorker(ModulationWorkerContext modulationContext)
        {
            this.ModuleDomainRepository = new ModuleDomainRepository();
            this.ModulationWorkerContext = modulationContext;
        }

        /// <summary>
        /// 开始工作
        /// </summary>
        public void Start()
        {
            foreach (Module module in this.ModulationWorkerContext.Modules)
            {
                ModuleDomainContext moduleDomainContext = new ModuleDomainContext();
                moduleDomainContext.AssemlbyLocation = module.Location;
                moduleDomainContext.ModuleDomainRepository = ModuleDomainRepository;
                moduleDomainContext.MisfitConnectionString = this.ModulationWorkerContext.MisfitConnectionString;

                foreach (string key in module.ConnectionStrings.Keys)
                {
                    moduleDomainContext.ConnectionStrings.Add(key, module.ConnectionStrings[key]);
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
        public object GetService(string assemblyCatalogName, string typeName)
        {
            return ModuleDomainRepository.GetService(assemblyCatalogName, typeName);
        }


        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            return ModuleDomainRepository.GetService<TInterface>(assemblyCatalogName);
        }


        /// <summary>
        /// 停止工作
        /// </summary>
        public void Stop()
        {
            ModuleDomainRepository.DestroyClose();
        }
    }
}
