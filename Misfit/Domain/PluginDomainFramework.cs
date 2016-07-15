using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 插件工作者
    /// </summary>
    public class PluginDomainFramework : MarshalByRefObject, IPluginDomainFramework
    {
        public List<Plugin> Plugins { private set; get; }
        public PluginDomainCollection PluginDomainCollection { private set; get; }

        public Dictionary<string, string> Variables { private set; get; }

        public string AddInsRoot { private set; get; }

        /// <summary>
        /// 有新的插件域初始化时，发生
        /// </summary>
        public event Action<IPluginDomainFramework, PluginDomain> OnModuleDomainInitialized;

        /// <summary>
        /// 有新的插件域安装时，发生
        /// </summary>
        public event Action<IPluginDomainFramework, PluginDomain> OnModuleDomainInstalled;

        public event Action<IPluginDomainFramework, PluginDomainException> OnModulationException;


        public PluginDomainFramework()
        {
            this.Plugins = new List<Plugin>();
            this.Variables = new Dictionary<string, string>();
            this.PluginDomainCollection = new PluginDomainCollection();
        }

        public PluginDomainFramework(List<Plugin> plugins, Dictionary<string, string> variables, string addInsRoot)
        {
            this.Plugins = plugins;
            this.Variables = variables;
            this.AddInsRoot = addInsRoot;
            this.PluginDomainCollection = new PluginDomainCollection();
        }

        #region 公有方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            foreach (Plugin plugin in this.Plugins)
            {
                PluginDomain pluginDomain = new PluginDomain(plugin, this);
                pluginDomain.OnInstalled += ModuleDomain_OnInstalled;
                pluginDomain.OnClosed += ModuleDomain_OnClosed;
                pluginDomain.OnException += ModuleDomain_OnException;

                this.PluginDomainCollection.Add(pluginDomain);
            }
        }



        /// <summary>
        /// 开始工作
        /// </summary>
        public void Start()
        {
            foreach (PluginDomain pluginDomain in this.PluginDomainCollection)
            {
                pluginDomain.Install();
            }
        }

        /// <summary>
        /// 获得相关插件域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            PluginDomain moduleDomain = this.PluginDomainCollection.Get(assemblyCatalogName);
            if (moduleDomain != null && moduleDomain.PluginDomainServices != null && moduleDomain.PluginDomainServices.Count > 0)
            {
                if (moduleDomain.PluginDomainServices.ContainsKey(typeName))
                    return moduleDomain.PluginDomainServices[typeName];
            }
            return null;
        }

        /// <summary>
        /// 获得相关插件域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            return (TInterface)this.GetService(assemblyCatalogName, typeof(TInterface).Name);
        }

        /// <summary>
        /// 停止工作
        /// </summary>
        public void Stop()
        {
            if (this.PluginDomainCollection.Any())
            {
                foreach (PluginDomain pluginDomain in this.PluginDomainCollection)
                {
                    pluginDomain.UnInstall();
                }
                this.PluginDomainCollection.Clear();
            }
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 插件域的初化始事件
        /// </summary>
        /// <param name="domain"></param>
        private void ModuleDomain_OnInitialized(PluginDomain domain)
        {
            if (this.OnModuleDomainInitialized != null)
                this.OnModuleDomainInitialized(this, domain);
        }

        /// <summary>
        /// 插件域的关闭事件
        /// </summary>
        /// <param name="domain"></param>
        private void ModuleDomain_OnClosed(PluginDomain domain)
        {

        }

        /// <summary>
        /// 插件域的安装事件
        /// </summary>
        /// <param name="domain"></param>
        private void ModuleDomain_OnInstalled(PluginDomain domain)
        {
            if (this.OnModuleDomainInstalled != null)
                this.OnModuleDomainInstalled(this, domain);
        }

        /// <summary>
        /// 发生异常
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="mex"></param>
        private void ModuleDomain_OnException(PluginDomain domain, PluginDomainException mex)
        {
            if (this.OnModulationException != null)
                this.OnModulationException(this, mex);
        }

        #endregion

    }
}
