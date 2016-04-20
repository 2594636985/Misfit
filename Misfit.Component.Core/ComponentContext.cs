using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn;

namespace Misfit.Component.Core
{
    public class ComponentContext : IComponentContext
    {
        #region IComponentContext Members

        public IDictionary<string, object> GetProperties()
        {
            throw new NotImplementedException();
        }

        public object LocateService(string name)
        {
            throw new NotImplementedException();
        }

        public object LocateService(string name, IServiceReference reference)
        {
            throw new NotImplementedException();
        }

        public IBundleContext BundleContext
        {
            get { throw new NotImplementedException(); }
        }

        public IBundle UsingBundle
        {
            get { throw new NotImplementedException(); }
        }

        public IComponentInstance ComponentInstance()
        {
            throw new NotImplementedException();
        }

        public void EnableComponent(string name)
        {
            throw new NotImplementedException();
        }

        public void DisableComponent(string name)
        {
            throw new NotImplementedException();
        }

        public IServiceReference GetServiceReference()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
