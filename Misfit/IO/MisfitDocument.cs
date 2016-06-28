using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Misfit.IO
{
    public class MisfitDocument : XmlDocument
    {
        public MisfitNode MisfitNode
        {
            get
            {
                XmlElement rootXmlElement = this.DocumentElement;
                if (rootXmlElement != null && rootXmlElement.HasChildNodes)
                {
                    MisfitNode misfitNode = new MisfitNode();

                    foreach (XmlNode xmlNode in rootXmlElement.ChildNodes)
                    {
                        if (string.Compare(xmlNode.Name, "Arguments", true) == 0)
                        {
                            if (xmlNode.HasChildNodes)
                            {
                                foreach (XmlNode moduleChildXmlNode in xmlNode.ChildNodes)
                                {
                                    if (string.Compare(moduleChildXmlNode.Name, "Arguments", true) == 0 && moduleChildXmlNode.HasChildNodes)
                                    {
                                        foreach (XmlNode ConnectionStringXmlNode in moduleChildXmlNode.ChildNodes)
                                        {
                                            ArgumentNode argumentNode = new ArgumentNode();

                                            XmlAttributeCollection connStrXmlAttrCollection = ConnectionStringXmlNode.Attributes;

                                            if (connStrXmlAttrCollection != null && connStrXmlAttrCollection.Count > 0)
                                            {
                                                foreach (XmlAttribute connStrXmlAttr in connStrXmlAttrCollection)
                                                {
                                                    if (string.Compare(connStrXmlAttr.Name, "Name", true) == 0)
                                                    {
                                                        argumentNode.Name = connStrXmlAttr.Value;
                                                    }
                                                    else if (string.Compare(connStrXmlAttr.Name, "Value", true) == 0)
                                                    {
                                                        argumentNode.Value = connStrXmlAttr.Value;
                                                    }
                                                }
                                            }

                                            misfitNode.ArgumentNodes.Add(argumentNode);
                                        }
                                    }
                                }
                            }
                        }
                        else if (string.Compare(xmlNode.Name, "Modules", true) == 0 && xmlNode.HasChildNodes)
                        {
                            foreach (XmlNode moduleXmlNode in xmlNode.ChildNodes)
                            {
                                ModuleNode moduleNode = new ModuleNode();

                                XmlAttributeCollection xmlAttributeCollection = moduleXmlNode.Attributes;
                                if (xmlAttributeCollection != null && xmlAttributeCollection.Count > 0)
                                {
                                    foreach (XmlAttribute beanXmlAttribute in xmlAttributeCollection)
                                    {
                                        if (string.Compare(beanXmlAttribute.Name, "Name", true) == 0)
                                        {
                                            moduleNode.Name = beanXmlAttribute.Value;
                                        }
                                        else if (string.Compare(beanXmlAttribute.Name, "Location", true) == 0)
                                        {
                                            moduleNode.Location = beanXmlAttribute.Value;
                                        }
                                        else if (string.Compare(beanXmlAttribute.Name, "Description", true) == 0)
                                        {
                                            moduleNode.Description = beanXmlAttribute.Value;
                                        }
                                    }
                                }

                                if (moduleXmlNode.HasChildNodes)
                                {
                                    foreach (XmlNode moduleChildXmlNode in moduleXmlNode.ChildNodes)
                                    {
                                        if (string.Compare(moduleChildXmlNode.Name, "Arguments", true) == 0 && moduleChildXmlNode.HasChildNodes)
                                        {
                                            foreach (XmlNode ConnectionStringXmlNode in moduleChildXmlNode.ChildNodes)
                                            {
                                                ArgumentNode connectionStringNode = new ArgumentNode();

                                                XmlAttributeCollection connStrXmlAttrCollection = ConnectionStringXmlNode.Attributes;

                                                if (connStrXmlAttrCollection != null && connStrXmlAttrCollection.Count > 0)
                                                {
                                                    foreach (XmlAttribute connStrXmlAttr in connStrXmlAttrCollection)
                                                    {
                                                        if (string.Compare(connStrXmlAttr.Name, "Name", true) == 0)
                                                        {
                                                            connectionStringNode.Name = connStrXmlAttr.Value;
                                                        }
                                                        else if (string.Compare(connStrXmlAttr.Name, "Value", true) == 0)
                                                        {
                                                            connectionStringNode.Value = connStrXmlAttr.Value;
                                                        }
                                                    }
                                                }

                                                moduleNode.ArgumentNodes.Add(connectionStringNode);
                                            }
                                        }
                                    }
                                }

                                misfitNode.ModuleNodes.Add(moduleNode);
                            }
                        }
                    }
                    return misfitNode;
                }
                return null;
            }
        }
    }
}
