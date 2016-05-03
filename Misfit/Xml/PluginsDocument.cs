using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Misfit.Xml
{
    public class PluginsDocument
    {
        private PluginNodeList _childNodes = new PluginNodeList();
        private XmlDocument _xmlDocument;
        public PluginsDocument()
        {
            this._xmlDocument = new XmlDocument();
        }

        public void Load(string path)
        {
            this._xmlDocument.Load(path);

            XmlElement rootXmlElement = this._xmlDocument.DocumentElement;
            if (rootXmlElement != null && rootXmlElement.HasChildNodes)
            {
                XmlAttributeCollection xmlAttributeCollection = rootXmlElement.Attributes;
                if (xmlAttributeCollection != null && xmlAttributeCollection.Count > 0)
                {
                    foreach (XmlAttribute xmlAttribute in xmlAttributeCollection)
                    {
                        if (string.Compare(xmlAttribute.Name, "Debug", true) == 0)
                        {
                            if (!string.IsNullOrWhiteSpace(xmlAttribute.Value))
                                this._childNodes.Debug = Convert.ToBoolean(xmlAttribute.Value);
                            else
                                this._childNodes.Debug = false;
                        }
                        else if (string.Compare(xmlAttribute.Name, "DebugName", true) == 0)
                        {
                            this._childNodes.DebugName = xmlAttribute.Value ;
                        }
                    }
                }

                foreach (XmlNode xmlNode in rootXmlElement)
                {
                    if (string.Compare(xmlNode.Name, "plugin", true) != 0)
                    {
                        throw new InvalidCastException("没有转化失败，存在非法节点");
                    }

                    PluginNode pluginNode = PluginNode.Parse(xmlNode);
                    if (pluginNode != null)
                    {
                        this._childNodes.Add(pluginNode);
                    }
                }
            }
        }

        public PluginNodeList ChildNodes
        {
            get
            {
                return this._childNodes;
            }

        }
    }
}
