using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 模块域的集合
    /// </summary>
    public class PluginDomainCollection : Collection<PluginDomain>
    {
        /// <summary>
        /// 根据模块域的模块名来获得模块域
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
