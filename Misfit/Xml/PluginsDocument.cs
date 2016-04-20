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
