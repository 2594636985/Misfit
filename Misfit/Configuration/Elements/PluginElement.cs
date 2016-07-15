using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 模块元素
    /// </summary>
    public class PluginElement : ConfigurationElement
    {
        public const string NameAttributeName = "Name";
        public const string LocationAttributeName = "Location";
        public const string DescriptionAttributeName = "Description";
        public const string DebugAttributeName = "Debug";
        public const string ParametersElementName = "Parameters";
        public const string UriElementName = "Uri";
        public const string TrackerElementName = "Tracker";

        /// <summary>
        /// 模块域的名称
        /// </summary>
        [ConfigurationProperty(NameAttributeName, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this[NameAttributeName];
            }
        }

        /// <summary>
        /// 程序集所在的位置
        /// </summary>
        [ConfigurationProperty(LocationAttributeName, IsRequired = true)]
        public string Location
        {
            get
            {
                return (string)this[LocationAttributeName];
            }
        }

        /// <summary>
        /// 模块的说明
        /// </summary>
        [ConfigurationProperty(DescriptionAttributeName, IsRequired = false)]
        public string Description
        {
            get
            {
                return (string)this[DescriptionAttributeName];
            }
        }

        /// <summary>
        /// 是否开启调试态状
        /// </summary>
        [ConfigurationProperty(DebugAttributeName, IsRequired = false, DefaultValue = "false")]
        public bool Debug
        {
            get
            {
                return Convert.ToBoolean(this[DebugAttributeName]);
            }
        }

        /// <summary>
        /// 额外带的参数
        /// </summary>
        [ConfigurationProperty(ParametersElementName, IsRequired = false)]
        public ParameterElementCollection Parameters
        {
            get
            {
                return (ParameterElementCollection)this[ParametersElementName];
            }
        }

        /// <summary>
        /// 对应URL
        /// </summary>
        [ConfigurationProperty(UriElementName, IsRequired = false)]
        public UriElement Uri
        {
            get
            {
                return (UriElement)this[UriElementName];
            }
        }

        /// <summary>
        /// 日志对应的名称
        /// </summary>
        [ConfigurationProperty(TrackerElementName, IsRequired = false)]
        public TrackerElement Tracker
        {
            get
            {
                return (TrackerElement)this[TrackerElementName];
            }
        }


    }
}
