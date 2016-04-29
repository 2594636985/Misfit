using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml;
using System.Xml.Serialization;
using Cramon.NetExtension.DynamicProxy;
using Misfit.AddIn;
using Misfit.Reflection;
using Misfit.Tracker;
using Misfit.Xml;

namespace Misfit.Core
{
    /// <summary>
    /// 每个Bundle的信息
    /// </summary>
    public class Plugin : IPlugin
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public int ModuleID { set; get; }

        /// <summary>
        /// 插件内核
        /// </summary>
        public IPluginFramework PluginFramework { set; get; }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { set; get; }


        /// <summary>
        /// 内部程序要用的名字
        /// </summary>
        public string Location { set; get; }

        /// <summary>
        /// 行为
        /// </summary>
        public PluginAction Action { set; get; }

        /// <summary>
        /// 是否为系统插件
        /// </summary>
        public bool IsSysPlugin { set; get; }


        public Plugin(string name, string location, string description, int level, string action, bool isSysPlugin)
        {
            this.Name = name;
            this.Description = description;
            this.Level = level;
            this.Location = location;
            this.IsSysPlugin = isSysPlugin;

            if (string.Compare(action, "Immediately", true) == 0)
                this.Action = PluginAction.Immediately;
            else
                this.Action = PluginAction.Delay;
        }
    }
}
