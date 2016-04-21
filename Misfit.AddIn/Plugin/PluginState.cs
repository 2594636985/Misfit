using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    /// <summary>
    /// 模块状态
    /// </summary>
    [Flags]
    public enum PluginState
    {
        /// <summary>
        /// 未安装
        /// </summary>
        Uninstalled = 1,

        /// <summary>
        /// 已安装
        /// </summary>
        Installed = 2,

        /// <summary>
        /// 停止.
        /// </summary>
        Stopped = 5,

        /// <summary>
        /// 激活.
        /// </summary>
        Actived = 6
    }
}
