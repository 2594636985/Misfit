using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Misfit.Xml
{
    public class PluginNode
    {
        public PluginNode()
        {

        }

        public string Name { set; get; }

        public string Level { set; get; }

        public string Description { set; get; }

        public string Location { set; get; }

        public string Action { set; get; }

        public static PluginNode Parse(XmlNode xmlNode)
        {
            if (xmlNode != null && xmlNode.Attributes != null && xmlNode.Attributes.Count > 0)
            {
                PluginNode pluginNode = new PluginNode();
                foreach (XmlAttribute xmlAttribute in xmlNode.Attributes)
                {
                    if (string.Compare(xmlAttribute.Name, "Name", true) == 0)
                    {
                        pluginNode.Name = xmlAttribute.Value;
                    }
                    else if (string.Compare(xmlAttribute.Name, "Location", true) == 0)
                    {
                        pluginNode.Location = xmlAttribute.Value;
                    }
                    else if (string.Compare(xmlAttribute.Name, "Description", true) == 0)
                    {
                        pluginNode.Description = xmlAttribute.Value;
                    }
                    else if (string.Compare(xmlAttribute.Name, "Action", true) == 0)
                    {
                        pluginNode.Action = xmlAttribute.Value;
                    }
                    else if (string.Compare(xmlAttribute.Name, "Level", true) == 0)
                    {
                        pluginNode.Level = xmlAttribute.Value;
                    }
                }

                return pluginNode;

            }
            return null;
        }
    }
}
