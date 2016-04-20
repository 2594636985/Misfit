using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Misfit.AddIn;
using Misfit.Component.Metadata;

namespace Misfit.Component.Core
{
    internal class ComponentManager : IComponentManager, IComponentInstance
    {
        private long id;
        
        private string name;
        
        private ComponentState state;
        
        private ComponentInfo component;
        
        private List<ComponentManager> dependencyManagers;
        
        private ComponentLoader loader;

        private IServiceRegistration serviceRegistration;

        public ComponentManager(long id, ComponentLoader loader,
            ComponentInfo component)
        {
            this.id = id;
            this.loader = loader;
            this.component = component;

            this.dependencyManagers = new List<ComponentManager>();
            this.state = ComponentState.Disabled;
        }

        #region IComponentManager Members

        public void Update()
        {
            throw new NotImplementedException();
        }

        public ComponentInfo Metadata
        {
            get { return component; }
        }

        #endregion

        #region IComponent Members

        public long Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return component.Name; }
        }

        public ComponentState State
        {
            get { return state; }
        }

        public IBundleContext Context
        {
            get { return loader.Context; }
        }

        public IBundle Bundle
        {
            get { return loader.Context.Bundle; }
        }

        public string Factory
        {
            get { return component.Factory; }
        }

        public bool IsServiceFactory
        {
            get
            {
                ProvideInfo[] providers = component.Service;
                return (providers != null && providers.Length > 0);
            }
        }

        public string ClassName
        {
            get
            {
                string className = string.Empty;

                if (component.Implementation != null)
                {
                    className = component.Implementation.Name;
                }

                return className;
            }
        }

        public bool IsDefaultEnabled
        {
            get { return component.Enabled; }
        }

        public bool IsImmediate
        {
            get { return component.Immediate; }
        }

        public string[] GetServices()
        {
            if (component.Service != null)
            {
                string[] services = new string[component.Service.Length];
                for (int i = 0; i < component.Service.Length; i++)
                {
                    services[i] = component.Service[i].Interface;
                }
                return services;
            }

            return null;
        }

        public IDictionary<string, object> GetProperties()
        {
            throw new NotImplementedException();
        }

        public ReferenceInfo[] getReferences()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            if (this.state == ComponentState.Destroyed)
            {
                return;
            }
            else if (this.state == ComponentState.Enabled)
            {
                throw new ComponentException("Component is already enabled " + component.Name + ".");
            }

            // If this component has got dependencies, create dependency managers for each one of them.

            // Load import assembly
            foreach (ImportInfo import in component.Runtime.Items)
            {
                //LoadAssembly(import.assembly);
            }

            // Load service
            Type implementation = LoadType(component.Implementation.Name);
            Type interfaceType = null;

            object serviceObject = Activator.CreateInstance(implementation);
            foreach (ProvideInfo provider in component.Service)
            {
                interfaceType = LoadType(provider.Interface);
                Context.RegisterService(interfaceType, serviceObject, null);
            }

            ReferenceInfo[] references = component.Reference;
            if (references != null)
            {
                // Load references
                foreach (ReferenceInfo reference in references)
                {
                    Type refInterfaceType = LoadType(reference.Interface);

                    string bindMethod = reference.Bind;
                    string unbindMethod = reference.Unbind;

                    object referenceServiceObject = loader.Context.GetService(refInterfaceType);
                    if (referenceServiceObject == null)
                    {
                        continue;
                    }

                    MethodInfo bindMethodInfo = refInterfaceType.GetMethod(bindMethod, new Type[] { interfaceType });
                    Debug.Assert(bindMethodInfo != null);

                    bindMethodInfo.Invoke(referenceServiceObject, new object[] { serviceObject });
                }
            }

            this.state = ComponentState.Enabled;

            ActivateInternal();
        }

        public void Disable()
        {
            this.state = ComponentState.Disabled;
        }

        private void ActivateInternal()
        {

        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion

        #region IComponentInstance Members

        public object Instance
        {
            get { return null; }
        }

        #endregion

        private Type LoadType(string typeName)
        {
            return Type.GetType(typeName);
        }
    }
}
