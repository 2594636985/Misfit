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

        public int Level { set; get; }

        public Version Version { set; get; }

        public string Description { set; get; }

        public string SymbolicName { set; get; }

        public string LifeScope { set; get; }

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
                    else if (string.Compare(xmlAttribute.Name, "SymbolicName", true) == 0)
                    {
                        pluginNode.SymbolicName = xmlAttribute.Value;
                    }
                    else if (string.Compare(xmlAttribute.Name, "Description", true) == 0)
                    {
                        pluginNode.Description = xmlAttribute.Value;
                    }
                    else if (string.Compare(xmlAttribute.Name, "LifeScope", true) == 0)
                    {
                        pluginNode.LifeScope = xmlAttribute.Value;
                    }
                    else if (string.Compare(xmlAttribute.Name, "Version", true) == 0)
                    {
                        if (!string.IsNullOrWhiteSpace(xmlAttribute.Value))
                            pluginNode.Version = new Version(xmlAttribute.Value);
                    }
                    else if (string.Compare(xmlAttribute.Name, "Level", true) == 0)
                    {
                        pluginNode.Level = Convert.ToInt32(xmlAttribute.Value);
                    }
                }

                return pluginNode;

            }
            return null;
        }
    }
}
