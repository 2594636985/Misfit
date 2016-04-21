using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public interface IPluginFramework
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle"></param>
        void InstallPlugin(IPlugin module);

        PluginsCollection PluginsCollection { get; }

        IPlugin StartPlugin(string symbolicName);

        void Start();

        void StopPlugin(string symbolicName);

        void UninstallPlugin(string symbolicName);

        AppDomain CreateDomain(IPluginContext context);

        void UnloadDomain(AppDomain domain);

        void Shutdown();
    }
}
