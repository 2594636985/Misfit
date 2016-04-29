using Autofac;
using Autofac.Core;
using Misfit.AddIn;
using Misfit.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace Misfit
{
    /// <summary>
    /// 模块运行
    /// </summary>
    public class PluginFramework : IPluginFramework
    {
        private PluginCollection _pluginsCollection = new PluginCollection();//用于加载存在要运行的插件
        private PluginRuntimeCollection _pluginRuntimeCollection = new PluginRuntimeCollection();//现在运行中的插件

        public event Action<IPluginFramework, IPlugin> OnPluginInstalled;

        public event Action<IPluginFramework, IPlugin> OnPluginStarted;

        public event Action<IPluginFramework, IPlugin> OnPluginStopped;

        public event Action<IPluginFramework, IPlugin> OnPluginUnInstalled;

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="plugin"></param>
        public void InstallPlugin(IPlugin plugin)
        {
            if (this._pluginsCollection.ContainsKey(plugin.Location))
            {
                throw new PluginException(string.Format("{0}已经安装了.", plugin.Location));
            }

            plugin.PluginFramework = this;

            this._pluginsCollection.Add(plugin.Name, plugin);

            if (this.OnPluginInstalled != null)
                this.OnPluginInstalled(this, plugin);

        }

        /// <summary>
        /// 插件集合
        /// </summary>
        public PluginCollection PluginsCollection
        {
            get { return this._pluginsCollection; }
        }

        /// <summary>
        /// 开始运行指定的插件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IPlugin StartPlugin(string name)
        {
            if (!this._pluginsCollection.ContainsKey(name))
                throw new PluginException(String.Format("插件[{0}]没有找到.", name));

            IPlugin plugin = this._pluginsCollection[name];

            if (this._pluginRuntimeCollection.Any(t => t.Plugin.Name == name))
                throw new PluginException(string.Format("插件{0}正在运行中.", name));

            PluginRuntime pluginRuntime = new PluginRuntime(plugin);
            this._pluginRuntimeCollection.Enqueue(pluginRuntime);
            pluginRuntime.Start();

            if (this.OnPluginStarted != null)
                this.OnPluginStarted(this, plugin);

            return plugin;
        }

        /// <summary>
        /// 开始运行插件框架
        /// </summary>
        public void Start()
        {
            if (this._pluginsCollection != null && this._pluginsCollection.Count > 0)
            {
                List<IPlugin> runningPlugins = this._pluginsCollection.Values.Where(t => t.Action == PluginAction.Immediately).ToList();
                if (runningPlugins != null && runningPlugins.Count > 0)
                {
                    foreach (IPlugin plugin in runningPlugins)
                    {
                        this.StartPlugin(plugin.Name);
                    }
                }

            }
        }

        /// <summary>
        /// 停止插件
        /// </summary>
        /// <param name="name"></param>
        public void StopPlugin(string name)
        {
            PluginRuntime pluginRuntime = this._pluginRuntimeCollection.FirstOrDefault(t => t.Plugin.Name == name);
            if (pluginRuntime != null)
            {
                pluginRuntime.Stop();

                if (this.OnPluginStopped != null)
                    this.OnPluginStopped(this, pluginRuntime.Plugin);
            }

        }


        /// <summary>
        /// 创建新的应用域
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public AppDomain CreateDomain(IPluginContext context)
        {
            AppDomainSetup info = new AppDomainSetup();
            info.ApplicationName = context.CurrentPlugin.Location;
            info.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
            info.DisallowApplicationBaseProbing = false;
            info.PrivateBinPath = Constants.AddInsFileRoot;
            info.ShadowCopyDirectories = Path.Combine(info.ApplicationBase, @"cache");
            info.ShadowCopyFiles = "true";
            string domainName = "Plugin-" + context.CurrentPlugin.Location;
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
            Evidence evidence = new Evidence(baseEvidence);
            return AppDomain.CreateDomain(domainName, evidence, info);
        }

        /// <summary>
        /// 移除应用域
        /// </summary>
        /// <param name="domain"></param>
        public void UnloadDomain(AppDomain domain)
        {
            if (domain != null)
            {
                AppDomain.Unload(domain);
            }
        }

        /// <summary>
        /// 关闭整个插件框架
        /// </summary>
        public void Shutdown()
        {
            for (int i = 0; i < this._pluginRuntimeCollection.Count; i++)
            {
                PluginRuntime pluginRuntime = this._pluginRuntimeCollection[i];
                pluginRuntime.Stop();
                this._pluginRuntimeCollection.Remove(pluginRuntime);
                i--;
            }
        }
    }
}
