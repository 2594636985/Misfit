using Misfit.Modulation.AddIn;
using Misfit.Modulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace Misfit.Modulation
{
    public class ModuleDomainRepository
    {
        public ModuleDomainCollection ModuleDomainCollection { private set; get; }

        public ModuleDomainRepository()
        {
            this.ModuleDomainCollection = new ModuleDomainCollection();
        }

        public void AddModulDomain(ModuleDomain moduleDomain)
        {
            if (this.ModuleDomainCollection.IsInstalled(moduleDomain.ModuleName))
                throw new InvalidDataException(string.Format("已经存在一个相同的{0}模块域", moduleDomain.ModuleName));

            this.ModuleDomainCollection.Add(moduleDomain);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            ModuleDomain moduleDomain = this.ModuleDomainCollection.Get(assemblyCatalogName);
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

        public void DestroyClose()
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
