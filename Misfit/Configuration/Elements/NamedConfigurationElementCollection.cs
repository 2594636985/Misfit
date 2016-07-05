using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 可以用键来获得的配置集合
    /// </summary>
    /// <typeparam name="TElementType"></typeparam>
    public class KeyConfigurationElementCollection<TElementType> : ConfigurationElementCollection, IEnumerable<TElementType>
        where TElementType : ConfigurationElement
    {
        private string _elementName;
        private string _elementKey;

        protected KeyConfigurationElementCollection(string elementName, string elementKey)
        {
            if (string.IsNullOrEmpty(elementName))
                throw new ArgumentNullException("elementName");
            if (string.IsNullOrWhiteSpace(elementKey))
                throw new ArgumentNullException("elementKey");

            this._elementName = elementName;
            this._elementKey = elementKey;
        }

        /// <summary>
        /// 节点名
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return this._elementName;
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        /// <summary>
        /// 判断是否为节点元素
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        protected override bool IsElementName(string elementName)
        {
            return elementName != null && elementName == _elementName;
        }


        public TElementType this[int index]
        {
            get
            {
                return base.BaseGet(index) as TElementType;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }


        protected override ConfigurationElement CreateNewElement()
        {
            return Activator.CreateInstance<TElementType>();
        }
        /// <summary>
        /// 设置如何根据键来获得对应的元素的值
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null) throw new ArgumentNullException("element");

            return (string)element.ElementInformation.Properties[_elementKey].Value;
        }

        public new IEnumerator<TElementType> GetEnumerator()
        {
            foreach (TElementType element in (IEnumerable)this)
                yield return element;
        }
    }
}
