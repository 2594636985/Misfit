using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Misfit.Plugins
{
    public class PluginDomainRepository
    {
         public PluginDomainCollection PluginDomainCollection { private set; get; }

         public PluginDomainRepository()
        {
            this.PluginDomainCollection = new PluginDomainCollection();
        }

        /// <summary>
        /// 增加模块域
        /// </summary>
        /// <param name="moduleDomain"></param>
        public void AddModulDomain(PluginDomain pluginDomain)
        {
            if (this.PluginDomainCollection.IsInstalled(pluginDomain.ModuleDomainName))
                throw new InvalidDataException(string.Format("已经存在一个相同的{0}插件域", pluginDomain.PluginDomainName));

            this.PluginDomainCollection.Add(pluginDomain);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            ModuleDomain moduleDomain = this.PluginDomainCollection.FirstOrDefault(pd =>pd.).Get(assemblyCatalogName);
            if (moduleDomain != null && moduleDomain.ModuleDomainServices != null && moduleDomain.ModuleDomainServices.Count > 0)
            {
                if (moduleDomain.ModuleDomainServices.ContainsKey(typeName))
                    return moduleDomain.ModuleDomainServices[typeName];
            }
            return null;
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            return (TInterface)this.GetService(assemblyCatalogName, typeof(TInterface).Name);
        }

        /// <summary>
        /// 清楚库存
        /// </summary>
        public void Clear()
        {
            if (this.ModuleDomainCollection.Any())
            {
                foreach (ModuleDomain moduleDomain in this.ModuleDomainCollection)
                {
                    moduleDomain.UnInstall();
                }
                this.ModuleDomainCollection.Clear();
            }
        }
    }
}
