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

        public ModulationWorkerContext()
        {
            this.Variables = new Dictionary<string, string>();
        }

        /// <summary>
        /// 模块的目标位置名称
        /// </summary>
        public string AddInsRoot { set; get; }

        /// <summary>
        /// 是否为调试态
        /// </summary>
        public bool IsDebug { set; get; }
        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, string> Variables { set; get; }

        /// <summary>
        /// 模块信息集合
        /// </summary>
        public List<Module> Modules { set; get; }

    }
}
