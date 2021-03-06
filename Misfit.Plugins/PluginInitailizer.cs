﻿using Misfit.Plugins.Core;
using Misfit.Plugins.Injection;
using Misfit.Plugins.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Plugins
{
    /// <summary>
    /// 用于初始化插件的类
    /// </summary>
    public class PluginInitailizer
    {

        public static void Start()
        {
            IPluginDomainContext pluginDomainContext = AppDomain.CurrentDomain.GetData("ModuleDomainContext") as IPluginDomainContext;
            string assemlbyLocation = AppDomain.CurrentDomain.GetData("AssemlbyLocation") as string;

            Assembly callingAssembly = AppDomain.CurrentDomain.Load(assemlbyLocation);
            PluginAssembly pluginAssembly = new PluginAssembly(callingAssembly);

            PluginConfiguration pluginConfiguration = new PluginConfiguration(pluginAssembly);
            pluginConfiguration.Initialize();

            AppDomain.CurrentDomain.SetData("PluginDomainName", pluginAssembly.Mainifest.Name);
            AppDomain.CurrentDomain.SetData("PluginDomainVersion", pluginAssembly.Version.ToString());

            MisfitContainerBuilder builder = new MisfitContainerBuilder();

            List<ModuleServiceType> moduleServiceTypes = pluginAssembly.ModuleServiceTypes;
            foreach (ModuleServiceType moduleServiceType in moduleServiceTypes)
            {
                if (moduleServiceType.InterfaceType == null)
                    throw new InvalidDataException(string.Format("{0}没有找到对应的接口类", moduleServiceType.ImplementType.Name));

                builder.Register(moduleServiceType.InterfaceType, moduleServiceType.ImplementType);
            }

            IPluginContainer misfitContainer = builder.Build();

            if (pluginAssembly.Mainifest != null)
            {

                Dictionary<string, object> pluginDomainServces = new Dictionary<string, object>();

                foreach (ServiceDescriptor beanDescriptor in pluginAssembly.Mainifest.ServiceDescriptors)
                {
                    ModuleServiceType boundaryModuleServiceType = moduleServiceTypes.FirstOrDefault(t => t.ImplementType.FullName == beanDescriptor.ClassName);
                    if (boundaryModuleServiceType == null)
                        throw new NullReferenceException("没有找配置里面对应的服务类型");

                    if (misfitContainer.HasRegistration(boundaryModuleServiceType.InterfaceType))
                    {
                        object instance = misfitContainer.Resolve(boundaryModuleServiceType.InterfaceType);

                        pluginDomainServces.Add(boundaryModuleServiceType.InterfaceType.Name, instance);
                    }
                }

                AppDomain.CurrentDomain.SetData("PluginDomainServcies", pluginDomainServces);
            }
        }

    }
}
