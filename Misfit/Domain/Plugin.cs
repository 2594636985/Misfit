using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 插件信息
    /// </summary>
    public class Plugin
    {
        public Plugin()
        {
            this.Parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// 是否插件是否开启调试状态
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
        public string AssemblyLocation { set; get; }

        /// <summary>
        /// 设置对应的URL用于来查看是否有最新版本
        /// </summary>
        public string Uri { set; get; }


        public Dictionary<string, string> Parameters { set; get; }
    }
}
