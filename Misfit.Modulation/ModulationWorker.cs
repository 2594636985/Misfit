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

        /// <summary>
        /// 有新的模块域初始化时，发生
        /// </summary>
        public event Action<IModulationWorker, ModuleDomain> OnModuleDomainInitialized;

        /// <summary>
        /// 有新的模块域安装时，发生
        /// </summary>
        public event Action<IModulationWorker, ModuleDomain> OnModuleDomainInstalled;

        public event Action<IModulationWorker, ModulationException> OnModulationException;

        public ModulationWorker(ModulationWorkerContext modulationContext)
        {
            this.ModuleDomainRepository = new ModuleDomainRepository();
            this.ModulationWorkerContext = modulationContext;
        }

        #region 公有方法

        public void Initialize()
        {
            foreach (Module module in this.ModulationWorkerContext.Modules)
            {
                ModuleDomainContext moduleDomainContext = new ModuleDomainContext();
                moduleDomainContext.AssemlbyLocation = module.Location;
                moduleDomainContext.ModuleDomainRepository = ModuleDomainRepository;

                foreach (string key in ModulationWorkerContext.Arguments.Keys)
                {
                    moduleDomainContext.MisfitArguments.Add(key, module.Arguments[key]);
                }

                foreach (string key in module.Arguments.Keys)
                {
                    moduleDomainContext.Arguments.Add(key, module.Arguments[key]);
                }

                ModuleDomain moduleDomain = new ModuleDomain(moduleDomainContext);
                moduleDomain.OnInitialized += ModuleDomain_OnInitialized;
                moduleDomain.OnInstalled += ModuleDomain_OnInstalled;
                moduleDomain.OnClosed += ModuleDomain_OnClosed;
                moduleDomain.OnException += ModuleDomain_OnException;
                moduleDomain.Initialize();

                ModuleDomainRepository.AddModulDomain(moduleDomain);
            }
        }



        /// <summary>
        /// 开始工作
        /// </summary>
        public void Start()
        {
            foreach (ModuleDomain moduleDomain in this.ModuleDomainRepository.ModuleDomainCollection)
            {
                moduleDomain.Install();
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
            ModuleDomainRepository.Clear();
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 模块域的初化始事件
        /// </summary>
        /// <param name="domain"></param>
        private void ModuleDomain_OnInitialized(ModuleDomain domain)
        {
            if (this.OnModuleDomainInitialized != null)
                this.OnModuleDomainInitialized(this, domain);
        }

        /// <summary>
        /// 模块域的关闭事件
        /// </summary>
        /// <param name="domain"></param>
        private void ModuleDomain_OnClosed(ModuleDomain domain)
        {

        }

        /// <summary>
        /// 模块域的安装事件
        /// </summary>
        /// <param name="domain"></param>
        private void ModuleDomain_OnInstalled(ModuleDomain domain)
        {
            if (this.OnModuleDomainInstalled != null)
                this.OnModuleDomainInstalled(this, domain);
        }

        /// <summary>
        /// 发生异常
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="mex"></param>
        private void ModuleDomain_OnException(ModuleDomain domain, ModuleException mex)
        {
            if (this.OnModulationException != null)
                this.OnModulationException(this, mex);
        }

        #endregion

    }
}
