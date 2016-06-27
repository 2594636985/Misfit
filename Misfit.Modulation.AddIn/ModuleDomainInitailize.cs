﻿using Misfit.Modulation.AddIn.Core;
using Misfit.Modulation.AddIn.Injection;
using Misfit.Modulation.AddIn.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Modulation.AddIn
{
    /// <summary>
    /// 用于初始化模块的类
    /// </summary>
    public class ModuleDomainInitailize
    {
        private static ModuleAssembly ModuleAssembly;
        /// <summary>
        /// 模块初始化
        /// </summary>
        public static void Initailize()
        {
            IModuleDomainContext moduleDomainContext = AppDomain.CurrentDomain.GetData("ModuleDomainContext") as IModuleDomainContext;
            string location = AppDomain.CurrentDomain.GetData("Location") as string;

            Assembly callingAssembly = AppDomain.CurrentDomain.Load(location);
            ModuleAssembly = new ModuleAssembly(callingAssembly);

            AddInConfiguration addInConfiguration = new AddInConfiguration(ModuleAssembly);
            addInConfiguration.Initialize();

            AppDomain.CurrentDomain.SetData("ModuleDomainName", ModuleAssembly.Mainifest.Name);
            AppDomain.CurrentDomain.SetData("ModuleDomainVersion", ModuleAssembly.Version.ToString());

        }

        public static void Start()
        {
            MisfitContainerBuilder builder = new MisfitContainerBuilder();

            List<ModuleServiceType> moduleServiceTypes = ModuleAssembly.ModuleServiceTypes;
            foreach (ModuleServiceType moduleServiceType in moduleServiceTypes)
            {
                if (moduleServiceType.InterfaceType == null)
                    throw new InvalidDataException(string.Format("{0}没有找到对应的接口类", moduleServiceType.ImplementType.Name));

                builder.Register(moduleServiceType.InterfaceType, moduleServiceType.ImplementType);
            }

            IMisfitContainer misfitContainer = builder.Build();

            if (ModuleAssembly.Mainifest != null)
            {

                Dictionary<string, object> moduleDomainServces = new Dictionary<string, object>();

                foreach (ServiceDescriptor beanDescriptor in ModuleAssembly.Mainifest.ServiceDescriptors)
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
