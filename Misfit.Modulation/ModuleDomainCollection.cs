using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块域的集合
    /// </summary>
    public class ModuleDomainCollection : List<ModuleDomain>
    {
        /// <summary>
        /// 根据模块域的模块名来获得模块域
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public ModuleDomain Get(string moduleName)
        {
            return this.FirstOrDefault(t => t.ModuleDomainName == moduleName);
        }

        /// <summary>
        /// 判断是否安装过
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool IsInstalled(string moduleName)
        {
            return this.Any(t => t.ModuleDomainName == moduleName);
        }
    }
}
