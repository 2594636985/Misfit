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
        public string MisfitConnectionString { set; get; }
        public List<Module> Modules { set; get; }

    }
}
