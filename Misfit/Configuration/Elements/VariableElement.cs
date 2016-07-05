using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 变量元素
    /// </summary>
    public class VariableElement : ConfigurationElement
    {
        public const string NameAttributeName = "Name";
        public const string ValueAttributeName = "Value";

        [ConfigurationProperty(NameAttributeName, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this[NameAttributeName];
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
