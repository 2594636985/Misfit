using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Misfit.AddIn;

namespace Misfit.Component.Core
{
    internal sealed class ComponentRegistry : IComponentRegistry
    {
        private Dictionary<long, IComponentManager> componentsById;
        private Dictionary<string, IComponentManager> componentsByName;

        private long componentCounter;
        private object syncObj;

        public ComponentRegistry(IBundleContext context)
        {
            this.componentsById = new Dictionary<long, IComponentManager>();
            this.componentsByName = new Dictionary<string, IComponentManager>();

            this.componentCounter = -1;
            this.syncObj = new object();
        }

        #region IComponentRegistry Members

        public long GenerateComponentId()
        {
            lock (syncObj)
            {
                Interlocked.Increment(ref this.componentCounter);
                return this.componentCounter;
            }
        }

        public void CheckComponentName(string name)
        {
            if (componentsByName.ContainsKey(name))
            {
                throw new ComponentException("The component name '" + name + "' has already been registered.");
            }
        }

        public void RegisterComponent(string name, IComponentManager component)
        {
            lock (syncObj)
            {
                CheckComponentName(name);

                componentsById.Add(component.Id, component);
                componentsByName.Add(name, component);
            }
        }

        public void UnregisterComponent(string name)
        {
            lock (syncObj)
            {
                IComponentManager component  = componentsByName[name];
                componentsByName.Remove(name);
                if (component != null)
                {
                    componentsById.Remove(component.Id);
                }
            }
        }

        public IComponentManager GetComponent(string name)
        {
            lock (syncObj)
            {
                return componentsByName[name];
            }
        }

        #endregion
    }
}
