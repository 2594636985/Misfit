using Misfit.Modulation.AddIn.Core;
using Misfit.Modulation.AddIn.Injection;
using Misfit.Modulation.AddIn.IO;
using Misfit.Modulation.AddIn.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.AddIn.Serices
{
    public abstract class MisfitAbsService : MarshalByRefObject, IMisfitService
    {
        private IMisfitContainer Container { set; get; }
        public ITracker Tracker { get { return AppDomain.CurrentDomain.GetData("ModuleDomainTracker") as ITracker; } }
        public IModuleDomainContext ModuleDomainContext { get { return AppDomain.CurrentDomain.GetData("ModuleDomainContext") as IModuleDomainContext; } }

        public MisfitAbsService(IMisfitContainer container)
        {
            this.Container = container;
        }

        public virtual object GetService(Type contractType)
        {
            return this.Container.Resolve(contractType);
        }

        public virtual TInterface GetService<TInterface>() where TInterface : IMisfitService
        {
            return this.Container.Resolve<TInterface>();
        }

        public virtual object GetService(string assemblyCatalogName, string typeName)
        {
            return this.ModuleDomainContext.GetService(assemblyCatalogName, typeName);
        }

        public virtual TInterface GetService<TInterface>(string assemblyCatalogName) where TInterface : IMisfitService
        {
            return this.ModuleDomainContext.GetService<TInterface>(assemblyCatalogName);
        }
    }
}
