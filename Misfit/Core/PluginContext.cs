using System;
using System.ComponentModel;
using System.IO;
using Misfit.AddIn;
using System.Collections.Generic;
using System.Diagnostics;

namespace Misfit.Core
{
    /// <summary>
    /// 每一次Bunlde的上下文
    /// </summary>
    public class PluginContext : IPluginContext
    {
        public IPluginFramework PluginFramework { private set; get; }
        public IPlugin CurrentPlugin { private set; get; }

        internal PluginContext(Plugin plugin)
        {
            this.CurrentPlugin = plugin;
            this.PluginFramework = plugin.PluginFramework;
        }

        public IPlugin GetPlugin(string symbolicName)
        {
            if (PluginFramework.PluginsCollection.ContainsKey(symbolicName))
                return PluginFramework.PluginsCollection[symbolicName];
            return null;
        }

        public void RegisterService(string[] clazzes, object service, Dictionary<string, object> properties)
        {
            throw new NotImplementedException();
        }

        public object GetService(string clazz)
        {
            throw new NotImplementedException();
        }

        public object GetService(Type type)
        {
            throw new NotImplementedException();
        }

        public T GetService<T>()
        {
            throw new NotImplementedException();
        }
    }
}