using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    public class ModuleDomainCollection : List<ModuleDomain>
    {
        public ModuleDomain Get(string moduleName)
        {
            return this.FirstOrDefault(t => t.ModuleName == moduleName);
        }

        /// <summary>
        /// 判断是否安装过
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool IsInstalled(string moduleName)
        {
            return this.Any(t => t.ModuleName == moduleName);
        }
    }
}
