using Misfit.Plugins.Core;
using Misfit.Plugins.Injection;
using Misfit.Plugins.Configuration;
using Misfit.Plugins.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Logic
{
    public abstract class PluginLogicService : MarshalByRefObject, IPluginLogicService
    {
        private IPluginContainer Container { set; get; }
        public ITracker Tracker { get { return AppDomain.CurrentDomain.GetData("ModuleDomainTracker") as ITracker; } }
        public IPluginDomainContext ModuleDomainContext { get { return AppDomain.CurrentDomain.GetData("ModuleDomainContext") as IPluginDomainContext; } }

        public PluginLogicService(IPluginContainer container)
        {
            this.Container = container;
        }

        public virtual object GetService(Type contractType)
        {
            return this.Container.Resolve(contractType);
        }

        public virtual TInterface GetService<TInterface>() where TInterface : IPluginLogicService
        {
            return this.Container.Resolve<TInterface>();
        }

        public virtual object GetService(string assemblyCatalogName, string typeName)
        {
            return this.ModuleDomainContext.GetService(assemblyCatalogName, typeName);
        }

        public virtual TInterface GetService<TInterface>(string assemblyCatalogName) where TInterface : IPluginLogicService
        {
            return this.ModuleDomainContext.GetService<TInterface>(assemblyCatalogName);
        }
    }
}
