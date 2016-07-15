using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 插件域的集合
    /// </summary>
    public class PluginDomainCollection : Collection<PluginDomain>
    {
        /// <summary>
        /// 根据插件域的插件名来获得插件域
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public PluginDomain Get(string moduleName)
        {
            return this.FirstOrDefault(t => t.PluginDomainName == moduleName);
        }

        /// <summary>
        /// 判断是否安装过
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool IsInstalled(string moduleName)
        {
            return this.Any(t => t.PluginDomainName == moduleName);
        }
    }
}
