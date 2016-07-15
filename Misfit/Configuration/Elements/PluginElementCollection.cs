using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Configuration.Elements
{
    /// <summary>
    /// 插件集合类
    /// </summary>
    public class PluginElementCollection : ConfigurationElementCollection<PluginElement>
    {
        public PluginElementCollection()
            : base("Plugin")
        {

        }
    }
}
