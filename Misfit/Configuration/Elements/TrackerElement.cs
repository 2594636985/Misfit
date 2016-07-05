using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 模块日志追踪元素
    /// </summary>
    public class TrackerElement : ConfigurationElement
    {
        public const string ValueAttributeName = "Value";

        [ConfigurationProperty(ValueAttributeName, IsRequired = false, DefaultValue = "Debug")]
        public string Value
        {
            get
            {
                return (string)this[ValueAttributeName];
            }
        }
    }
}
