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
            this.ConnectionStrings = new Dictionary<string, string>();
        }
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
        /// 数据连接字符串
        /// </summary>
        public Dictionary<string, string> ConnectionStrings { set; get; }
    }
}
