using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Misfit.AddIn;
using Misfit.Component.Metadata;

namespace Misfit.Component.Core
{
    [Serializable]
    [AddIn("ComponentAddIn")]
    public class ComponentAddIn : AddInBase
    {
        private IBundleContext context;
        private IComponentRegistry componentRegistory;
        private List<ComponentLoader> componentBundles;

        public override void Start(IBundleContext context)
        {
            this.context = context;
            this.componentRegistory = new ComponentRegistry(context);
            this.componentBundles = new List<ComponentLoader>();

            this.context.BundleEvent += new BundleEventHandler(OnBundleEvent);

            LoadAllComponents(context);
        }

        public override void Stop(IBundleContext context)
        {
            this.context = null;
            this.componentRegistory = null;
        }
        
        void OnBundleEvent(object sender, BundleEventArgs e)
        {
            if (e.Transition == BundleTransition.Started)
            {
                LoadComponents(e.Bundle);
            }
            else if (e.Transition == BundleTransition.Stopping)
            {
                DisposeComponents(e.Bundle);
            }
        }

        private void LoadAllComponents(IBundleContext context)
        {
            IBundle[] bundles = context.Bundles;
            foreach (IBundle bundle in bundles)
            {
                if (bundle.State == BundleState.Active)
                {
                    LoadComponents(bundle);
                }
            }
        }

        private void DisposeAllComponents()
        {
            // TODO: Dispose all components.
        }

        private void DisposeComponents(IBundle bundle)
        {

        }

        private void LoadComponents(IBundle bundle)
        {
            ComponentMetadata componentMetadata = ComponentLoader.GetComponentMetadata(bundle);
            if (componentMetadata == null)
            {
                return;
            }

            IBundleContext context = bundle.Context;
            if (context == null)
            {
                throw new ComponentException("Cannot get BundleContext of bundle " +
                    bundle.SymbolicName + ".");
            }

            try
            {
                ComponentLoader loader = new ComponentLoader(componentRegistory, context);
                componentBundles.Add(loader);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
