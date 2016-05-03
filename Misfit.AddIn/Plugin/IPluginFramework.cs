using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public interface IPluginFramework
    {
        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="plugin"></param>
        void InstallPlugin(IPlugin plugin);

        /// <summary>
        /// 插件集合
        /// </summary>
        PluginCollection PluginsCollection { get; }

        /// <summary>
        /// 开始运行指定的插件
        /// </summary>
        /// <param name="symbolicName"></param>
        /// <returns></returns>
        IPlugin StartPlugin(string symbolicName);

        /// <summary>
        /// 开始运行插件框架
        /// </summary>
        void Start();

        /// <summary>
        /// 停止插件
        /// </summary>
        /// <param name="symbolicName"></param>
        void StopPlugin(string symbolicName);

        /// <summary>
        /// 创建新的应用域
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        AppDomain CreateDomain(string location);

        /// <summary>
        /// 移除应用域
        /// </summary>
        /// <param name="domain"></param>
        void UnloadDomain(AppDomain domain);
        /// <summary>
        /// 关闭整个插件框架
        /// </summary>
        void Shutdown();
    }
}
