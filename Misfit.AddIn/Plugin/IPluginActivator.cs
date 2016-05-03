using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    /// <summary>
    /// 模块启动者
    /// </summary>
    public interface IPluginActivator
    {
        void Start(string[] args);

        void Stop(string[] args);
    }
}
