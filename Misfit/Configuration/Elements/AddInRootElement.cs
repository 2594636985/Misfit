using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 插件存在的目标
    /// </summary>
    public class AddInRootElement : ConfigurationElement
    {
        public const string ValueAttributeName = "Value";

        [ConfigurationProperty(ValueAttributeName, IsRequired = false, DefaultValue = "AddIns")]
        public string Value
        {
            get
            {
                return (string)this[ValueAttributeName];
            }
        }
    }
}
