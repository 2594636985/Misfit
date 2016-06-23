using Misfit.AddIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Core
{
    /// <summary>
    /// 插件服务的上下文
    /// </summary>
    public class ModuleDomainContext : MarshalByRefObject, IModuleDomainContext
    {
        public string ModuleDomainName { set; get; }
        public string MisfitConnectionString { set; get; }
        public string AssemlbyLocation { set; get; }
        public ModuleDomainRepository ModuleDomainRepository { set; get; }

        public Dictionary<string, string> ConnectionStrings { set; get; }

        public ModuleDomainContext()
        {
            this.ConnectionStrings = new Dictionary<string, string>();
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public object GetService(string assemblyCatalogName, string typeName)
        {
            return this.ModuleDomainRepository.GetService(assemblyCatalogName, typeName);
        }

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        public TInterface GetService<TInterface>(string assemblyCatalogName) 
        {
            return this.ModuleDomainRepository.GetService<TInterface>(assemblyCatalogName);
        }
    }
}
