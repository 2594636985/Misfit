using Misfit.Modulation.AddIn;
using Misfit.Modulation.Tracking;
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
        /// <summary>
        /// 模块的目标位置名称
        /// </summary>
        public string AddInsRoot { set; get; }
        /// <summary>
        /// 是否为调试状态
        /// </summary>
        public bool IsDebug { set; get; }

        /// <summary>
        /// 用追踪的方式
        /// </summary>
        public TrackerTarget TrackerTarget { set; get; }

        /// <summary>
        /// 模块域的名称
        /// </summary>
        public string ModuleDomainName { set; get; }
        /// <summary>
        /// 程序集的所在的位置
        /// </summary>
        public string AssemlbyLocation { set; get; }
        /// <summary>
        /// 框架变量
        /// </summary>
        public Dictionary<string, string> Variables { set; get; }
        /// <summary>
        /// 模块域的库存
        /// </summary>
        public ModuleDomainRepository ModuleDomainRepository { set; get; }
        /// <summary>
        /// 参数
        /// </summary>
        public Dictionary<string, string> Parameters { set; get; }

        public ModuleDomainContext()
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
