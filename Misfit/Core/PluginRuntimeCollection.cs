using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Core
{
    public class PluginRuntimeCollection : List<PluginRuntime>
    {
        /// <summary>
        /// 增加到之后位置
        /// </summary>
        /// <param name="pluginRuntime"></param>
        public void Enqueue(PluginRuntime pluginRuntime)
        {
            this.Add(pluginRuntime);
        }
    }
}
