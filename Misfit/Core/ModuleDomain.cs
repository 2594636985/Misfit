using Misfit.AddIn;
using Misfit.AddIn.Core;
using Misfit.AddIn.Injection;
using Misfit.AddIn.IO;
using Misfit.AddIn.Serices;
using Misfit.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Core
{
    /// <summary>
    /// 模块域
    /// </summary>
    public class ModuleDomain
    {
        /// <summary>
        /// 模块域的哉名
        /// </summary>
        public string DomainName { private set; get; }

        /// <summary>
        /// 模块域的名称
        /// </summary>
        public string ModuleName { private set; get; }

        /// <summary>
        /// 模块域上下文
        /// </summary>
        public ModuleDomainContext ModuleDomainContext { private set; get; }

        /// <summary>
        /// 对应的应用域
        /// </summary>

        public AppDomain Domain { private set; get; }

        /// <summary>
        /// 是否安装过
        /// </summary>
        public bool Installed { private set; get; }

        /// <summary>
        /// 模块域的对外服务
        /// </summary>
        public Dictionary<string, object> ModuleDomainServices { private set; get; }


        public ModuleDomain(ModuleDomainContext moduleDomainContext)
        {
            this.ModuleDomainContext = moduleDomainContext;
            this.DomainName = moduleDomainContext.ModuleDomainName;
            this.Installed = false;
        }

        /// <summary>
        /// 安装
        /// </summary>
        public void Install()
        {
            this.Domain = ModuleDomainFactory.CreateDomain("Module-" + this.ModuleDomainContext.AssemlbyLocation);
            this.Domain.SetData("Location", this.ModuleDomainContext.AssemlbyLocation);
            this.Domain.SetData("ModuleDomainContext", this.ModuleDomainContext);

            this.Domain.DoCallBack(ModuleDomainInitailize);

            this.ModuleName = this.Domain.GetData("ModuleDomainName") as string;
            this.ModuleDomainServices = this.Domain.GetData("ModuleDomainServcies") as Dictionary<string, object>;

            this.Installed = true;

        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void UnInstall()
        {
            if (this.ModuleDomainServices != null)
                this.ModuleDomainServices.Clear();

            if (this.Domain != null)
                AppDomain.Unload(this.Domain);

            this.Installed = false;
        }

        /// <summary>
        /// 模块初始化
        /// </summary>
        private static void ModuleDomainInitailize()
        {
            IModuleDomainContext moduleDomainContext = AppDomain.CurrentDomain.GetData("ModuleDomainContext") as IModuleDomainContext;
            string location = AppDomain.CurrentDomain.GetData("Location") as string;

            Assembly callingAssembly = AppDomain.CurrentDomain.Load(location);
            ModuleAssembly moduleAssembly = new ModuleAssembly(callingAssembly);

            AddInConfiguration addInConfiguration = new AddInConfiguration(moduleAssembly);
            addInConfiguration.Initialize();

            MisfitContainerBuilder builder = new MisfitContainerBuilder();

            List<ModuleServiceType> moduleServiceTypes = moduleAssembly.ModuleServiceTypes;
            foreach (ModuleServiceType moduleServiceType in moduleServiceTypes)
            {
                if (moduleServiceType.InterfaceType == null)
                    throw new InvalidDataException(string.Format("{0}没有找到对应的接口类", moduleServiceType.ImplementType.Name));

                builder.Register(moduleServiceType.InterfaceType, moduleServiceType.ImplementType);
            }

            IMisfitContainer misfitContainer = builder.Build();

            MainifestDescriptor mainifestDescriptor = addInConfiguration.MainifestDescriptor;

            if (mainifestDescriptor != null)
            {
                AppDomain.CurrentDomain.SetData("ModuleDomainName", mainifestDescriptor.Name);

                Dictionary<string, object> moduleDomainServces = new Dictionary<string, object>();

                foreach (ServiceDescriptor beanDescriptor in mainifestDescriptor.ServiceDescriptors)
                {
                    ModuleServiceType boundaryModuleServiceType = moduleServiceTypes.FirstOrDefault(t => t.ImplementType.FullName == beanDescriptor.ClassName);
                    if (boundaryModuleServiceType == null)
                        throw new NullReferenceException("没有找配置里面对应的服务类型");

                    if (misfitContainer.HasRegistration(boundaryModuleServiceType.InterfaceType))
                    {
                        object instance = misfitContainer.Resolve(boundaryModuleServiceType.InterfaceType);

                        moduleDomainServces.Add(boundaryModuleServiceType.InterfaceType.Name, instance);
                    }
                }

                AppDomain.CurrentDomain.SetData("ModuleDomainServcies", moduleDomainServces);
            }

        }

    }
}
