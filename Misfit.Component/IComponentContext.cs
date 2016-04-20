using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn;

namespace Misfit.Component
{
    public interface IComponentContext
    {
        IDictionary<string, object> GetProperties();
        
        object LocateService(string name);

        object LocateService(string name, IServiceReference reference);

        IBundleContext BundleContext { get; }

        IBundle UsingBundle { get; }

        IComponentInstance ComponentInstance();

        void EnableComponent(string name);

        void DisableComponent(string name);

        IServiceReference GetServiceReference();
    }
}
