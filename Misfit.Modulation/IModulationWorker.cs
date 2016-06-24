using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Modulation
{
    public interface IModulationWorker
    {
        /// <summary>
        /// 开始工作
        /// </summary>
        void Start();

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

        /// <summary>
        /// 停止工作
        /// </summary>
        void Stop();
    }
}
