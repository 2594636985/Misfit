using Misfit.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 模块域的上下文
    /// </summary>
    public class PluginDomainContext : MarshalByRefObject, IPluginDomainContext
    {
        public IPluginDomainFramework PluginDomainFramework { set; get; }

        /// <summary>
        /// 是否为调试状态
        /// </summary>
        public bool IsDebug { set; get; }

        /// <summary>
        /// 框架变量
        /// </summary>
        public Dictionary<string, string> Variables { set; get; }

        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, string> Parameters { set; get; }

        public PluginDomainContext()
        {
            this.Variables = new Dictionary<string, string>();
            this.Parameters = new Dictionary<string, string>();
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            return this.PluginDomainFramework.GetService(assemblyCatalogName, typeName);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName)
        {
            return this.PluginDomainFramework.GetService<TInterface>(assemblyCatalogName);
        }
    }
}
