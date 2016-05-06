using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.AddIn
{

    /// <summary>
    /// 模块
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        int PluginID { set; get; }

        /// <summary>
        /// 插件内核
        /// </summary>
        IPluginFramework PluginFramework { set; get; }

        /// <summary>
        /// 插件名称
        /// </summary>
        string Name { set; get; }

        /// <summary>
        /// 等级
        /// </summary>
        int Level { set; get; }
        /// <summary>
        /// 
        /// </summary>
        string Description { set; get; }


        /// <summary>
        /// 内部程序要用的名字
        /// </summary>
        string Location { set; get; }


        /// <summary>
        /// 激活类
        /// </summary>
        string Activator { set; get; }

        /// <summary>
        /// 行为
        /// </summary>
        PluginAction Action { set; get; }

        /// <summary>
        /// 是否为系统插件
        /// </summary>
        bool IsSysPlugin { set; get; }

        string Extension { set; get; }
    }
}
