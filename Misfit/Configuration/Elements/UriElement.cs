using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 模块URI元素
    /// </summary>
    public class UriElement : ConfigurationElement
    {
        public const string ValueAttributeName = "Value";

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
