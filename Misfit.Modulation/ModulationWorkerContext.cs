using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块工作者的上下文
    /// </summary>
    public class ModulationWorkerContext
    {
        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, string> Arguments { set; get; }

        /// <summary>
        /// 模块信息集合
        /// </summary>
        public List<Module> Modules { set; get; }

    }
}
