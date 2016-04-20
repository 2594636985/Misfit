using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Misfit.AddIn;
using Misfit.AddIn.Utility;
using Misfit.Component.Metadata;

namespace Misfit.Component.Core
{
    /// <summary>
    /// Is a Helper to load or unload components of a single bundle.
    /// </summary>
    internal class ComponentLoader
    {
        private IComponentRegistry componentRegistory;
        private IBundleContext context;

        public IBundleContext Context
        {
            get
            {
                return context;
            }
        }

        public ComponentLoader(IComponentRegistry componentRegistory, IBundleContext context)
        {
            this.componentRegistory = componentRegistory;
            this.context = context;

            Initialize();
        }

        private void Initialize()
        {
            ComponentMetadata metadata = ComponentLoader.GetComponentMetadata(this.context.Bundle);
            if (metadata == null)
            {
                return;
            }

            foreach (ComponentInfo component in metadata.Component)
            {
                // check and reserve the component name
                componentRegistory.CheckComponentName(metadata.Name);

                IComponentManager componentManager = new ComponentManager(componentRegistory.GenerateComponentId(),
                    this, component);

                componentRegistory.RegisterComponent(metadata.Name, componentManager);

                if (component.Enabled)
                {
                    componentManager.Enable();
                }
            }
        }

        internal static ComponentMetadata GetComponentMetadata(IBundle bundle)
        {
            string addinFile = bundle.Location.Replace(".dll", ".addin");
            if (File.Exists(addinFile))
            {
                string xml = string.Empty;
                using (StreamReader reader = File.OpenText(addinFile))
                {
                    xml = reader.ReadToEnd();
                }
                return (ComponentMetadata)XmlConvertor.XmlToObject(typeof(ComponentMetadata), xml);
            }

            return null;
        }
    }
}
