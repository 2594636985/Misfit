using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Misfit.AddIn.IO
{
    public class MainifestDocument : XmlDocument
    {
        public MainifestNode MainifestNode
        {
            get
            {
                XmlElement rootXmlElement = this.DocumentElement;
                if (rootXmlElement != null && rootXmlElement.HasChildNodes)
                {
                    MainifestNode mainifestNode = new IO.MainifestNode();
                    foreach (XmlNode xmlNode in rootXmlElement)
                    {
                        if (string.Compare(xmlNode.Name, "Name", true) == 0)
                        {
                            mainifestNode.Name = xmlNode.InnerText;
                        }
                        else if (string.Compare(xmlNode.Name, "ConnectString", true) == 0)
                        {
                            mainifestNode.ConnectString = xmlNode.InnerText;
                        }
                        else if (string.Compare(xmlNode.Name, "Servcies", true) == 0 && xmlNode.HasChildNodes)
                        {
                            foreach (XmlNode serviceXmlNode in xmlNode.ChildNodes)
                            {
                                ServiceNode serviceNode = new ServiceNode();
                                XmlAttributeCollection xmlAttributeCollection = serviceXmlNode.Attributes;
                                if (xmlAttributeCollection != null && xmlAttributeCollection.Count > 0)
                                {
                                    foreach (XmlAttribute beanXmlAttribute in xmlAttributeCollection)
                                    {
                                        if (string.Compare(beanXmlAttribute.Name, "Name", true) == 0)
                                        {
                                            serviceNode.Name = beanXmlAttribute.Value;
                                        }
                                        else if (string.Compare(beanXmlAttribute.Name, "ClassName", true) == 0)
                                        {
                                            serviceNode.ClassName = beanXmlAttribute.Value;
                                        }
                                    }
                                }
                                mainifestNode.ServiceNodes.Add(serviceNode);
                            }
                        }
                    }
                    return mainifestNode;
                }
                return null;
            }
        }
    }
}
