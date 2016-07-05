using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 模块付带的参数元素
    /// </summary>
    public class ParameterElement : ConfigurationElement
    {
        internal const string KeyAttributeName = "Key";
        internal const string ValueAttributeName = "Value";

        [ConfigurationProperty(KeyAttributeName, IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)this[KeyAttributeName];
            }
        }

        [ConfigurationProperty(ValueAttributeName, IsRequired = false)]
        public string Value
        {
            get
            {
                return (string)this[ValueAttributeName];
            }
        }
    }
}
