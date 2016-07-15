using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 模块工作者接口
    /// </summary>
    public interface IPluginDomainFramework
    {
        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <param name="assemblyCatalogName"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        object GetService(string assemblyCatalogName, string typeName);

        /// <summary>
        /// 获得相关模块域里面的对外服务
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="assemblyCatalogName"></param>
        /// <returns></returns>
        TInterface GetService<TInterface>(string assemblyCatalogName);

    }
}
