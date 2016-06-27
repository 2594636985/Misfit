using Misfit.Modulation.AddIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块域的上下文
    /// </summary>
    public class ModuleDomainContext : MarshalByRefObject, IModuleDomainContext
    {
        public string ModuleDomainName { set; get; }
        /// <summary>
        /// 程序集的所在的位置
        /// </summary>
        public string AssemlbyLocation { set; get; }
        /// <summary>
        /// 框架参数
        /// </summary>
        public Dictionary<string, string> MisfitArguments { set; get; }

        public ModuleDomainRepository ModuleDomainRepository { set; get; }
        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, string> Arguments { set; get; }

        public ModuleDomainContext()
        {
            this.Arguments = new Dictionary<string, string>();
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
