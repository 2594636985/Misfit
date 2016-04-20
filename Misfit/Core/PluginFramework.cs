using Autofac;
using Autofac.Core;
using Misfit.AddIn;
using Misfit.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Misfit
{
    /// <summary>
    /// 模块运行
    /// </summary>
    public class PluginFramework : IPluginFramework
    {
        public const string AddInsFileRoot = "AddIns";
        private const string BundleExtention = ".dll";
        private PluginsCollection _pluginsCollection = new PluginsCollection();
        private int _bundleAppDomains;
        private int _serviceId;

        public event Action<PluginFramework, IPlugin> OnModuleInstalled;


        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="bundle"></param>
        public void InstallPlugin(IPlugin module)
        {
            if (this._pluginsCollection.ContainsKey(module.SymbolicName))
            {
                throw new BundleException(string.Format("{0}已经安装了.", module.SymbolicName));
            }

            this._pluginsCollection.Add(module.SymbolicName, module);

            if (this.OnModuleInstalled != null)
                this.OnModuleInstalled(this, module);

        }

        public PluginsCollection PluginsCollection
        {
            get { return this._pluginsCollection; }
        }

        public IPlugin StartPlugin(string symbolicName)
        {
            if (!this._pluginsCollection.ContainsKey(symbolicName))
                throw new BundleException(String.Format("插件[{0}]没有找到.", symbolicName));

            IPlugin plugin = this._pluginsCollection[symbolicName];


            if (plugin.PluginState != PluginState.Installed)
            {
                throw new BundleException("Bundle 正在运行中.");
            }

            plugin.Start();

            return plugin;
        }

        public void Start()
        {
            foreach (IPlugin plugin in this._pluginsCollection.Values)
            {
                if (plugin.PluginState != PluginState.Actived)
                {
                    plugin.Start();
                }
            }
        }

        public void StopPlugin(string symbolicName)
        {

            if (!this._pluginsCollection.ContainsKey(symbolicName))
                throw new BundleException(String.Format("插件[{0}]没有找到.", symbolicName));

            IPlugin plugin = this._pluginsCollection[symbolicName];

            if (plugin.PluginState != PluginState.Stopped)
            {
                plugin.Stop();
            }
        }

        public void UninstallPlugin(string symbolicName)
        {
            if (!this._pluginsCollection.ContainsKey(symbolicName))
                throw new BundleException(String.Format("插件[{0}]没有找到.", symbolicName));

            IPlugin plugin = this._pluginsCollection[symbolicName];

            if (plugin.PluginState == PluginState.Actived)
            {
                throw new BundleException(String.Format("插件[{0}]正在运行，请先关闭", symbolicName));
            }

            if (plugin.PluginState != PluginState.Stopped)
            {
                plugin.Stop();
            }
        }

        public AppDomain CreateDomain(IPluginContext context)
        {
            AppDomainSetup info = new AppDomainSetup();
            info.ApplicationBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AddIns");
            info.ShadowCopyDirectories = Path.Combine(info.ApplicationBase, @"cache");
            info.ShadowCopyFiles = "true";
            string domainName = "Plugin-" + context.CurrentPlugin.SymbolicName;
            AppDomain domain = AppDomain.CreateDomain(domainName, AppDomain.CurrentDomain.Evidence, info);

            Interlocked.Increment(ref this._bundleAppDomains);

            return domain;
        }

        public void UnloadDomain(AppDomain domain)
        {
            if (domain != null)
            {
                AppDomain.Unload(domain);
                Interlocked.Decrement(ref this._bundleAppDomains);
            }
        }

        public void Shutdown()
        {
            List<string> removePluginKeys = this._pluginsCollection.Keys.ToList();
            for (int i = removePluginKeys.Count - 1; i >= 0; i--)
            {
                string pluginKey = removePluginKeys[i];
                IPlugin plugin = this._pluginsCollection[pluginKey];
                StopPlugin(plugin.SymbolicName);
                UninstallPlugin(plugin.SymbolicName);
                this._pluginsCollection.Remove(pluginKey);
                plugin = null;
            }
        }
    }
}
