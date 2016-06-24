using Misfit.Modulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Modulation.IO
{
    public class MisfitConfiguration
    {
        public string ConfigurationString { private set; get; }
        public MisfitDescriptor MisfitDescriptor { private set; get; }
        public MisfitConfiguration()
            : this("Misfit.xml")
        {

        }
        public MisfitConfiguration(string configurationString)
        {
            this.ConfigurationString = configurationString;
        }

        public void Initialize()
        {
            if (!Path.IsPathRooted(this.ConfigurationString))
                this.ConfigurationString = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.ConfigurationString);

            MisfitDocument pluginDocument = new MisfitDocument();
            pluginDocument.Load(this.ConfigurationString);

            MisfitNode misfitNode = pluginDocument.MisfitNode;
            if (misfitNode != null)
            {
                this.MisfitDescriptor = new MisfitDescriptor();
                this.MisfitDescriptor.MisfitConnectionString = misfitNode.MisfitConnectString;

                foreach (ModuleNode pluginNode in misfitNode.PluginNodes)
                {
                    Module pluginDescriptor = new Module();
                    pluginDescriptor.Description = pluginNode.Description;
                    pluginDescriptor.Name = pluginNode.Name;
                    pluginDescriptor.Location = pluginNode.Location;

                    foreach(ConnectionStringNode connectionStringNode in pluginNode.ConnectionStringNodes)
                    {
                        pluginDescriptor.ConnectionStrings.Add(connectionStringNode.Name,connectionStringNode.Value);
                    }

                    this.MisfitDescriptor.PluginDescriptors.Add(pluginDescriptor);
                }
            }
        }
    }
}
