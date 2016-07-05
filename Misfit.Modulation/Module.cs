using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class Module
    {
        public Module()
        {
            this.Arguments = new Dictionary<string, string>();
        }

        /// <summary>
        /// 是否模块是否开启调试状态
        /// </summary>
        public bool IsDebug { set; get; }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 内部程序要用的名字
        /// </summary>
        public string Location { set; get; }

        /// <summary>
        /// 设置对应的URL用于来查看是否有最新版本
        /// </summary>
        public string Uri { set; get; }

        /// <summary>
        /// 用于模块是要用什么来追踪  
        /// </summary>

        public Tracking.TrackerTarget TrackerTarget { set; get; }

        /// <summary>
        /// 数据连接字符串
        /// </summary>
        public Dictionary<string, string> Arguments { set; get; }
    }
}
