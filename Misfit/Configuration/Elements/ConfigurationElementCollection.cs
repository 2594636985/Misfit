using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 配置集合类
    /// </summary>
    /// <typeparam name="TElementType"></typeparam>
    public class ConfigurationElementCollection<TElementType> : ConfigurationElementCollection, IEnumerable<TElementType>
      where TElementType : ConfigurationElement
    {
        private readonly string _elementName;

        public ConfigurationElementCollection(string elementName)
        {
            _elementName = elementName;
        }
        /// <summary>
        /// 节点名
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return _elementName;
            }
        }
        /// <summary>
        /// 判断是否为节点元素
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        protected override bool IsElementName(string elementName)
        {
            return !string.IsNullOrEmpty(elementName) && elementName == _elementName;
        }


        public new IEnumerator<TElementType> GetEnumerator()
        {
            foreach (TElementType element in (IEnumerable)this)
                yield return element;
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return Activator.CreateInstance<TElementType>();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return Guid.NewGuid();
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
    }
}
