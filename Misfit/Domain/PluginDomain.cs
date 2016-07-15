
using Misfit.Aspect;
using Misfit.Extension;
using Misfit.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Misfit.Domain
{
    /// <summary>
    /// 插件域
    /// </summary>
    public class PluginDomain
    {
        #region 公有成员

        /// <summary>
        /// 插件域的名称
        /// </summary>
        public string PluginDomainName { private set; get; }

        /// <summary>
        /// 插件的参数
        /// </summary>
        public Dictionary<string, string> Parameters { private set; get; }

        /// <summary>
        /// 插件域的版本号
        /// </summary>
        public Version PluginDomainVersion { private set; get; }


        public PluginDomainFramework PluginDomainFramework { private set; get; }

        /// <summary>
        /// 插件域上下文
        /// </summary>
        public PluginDomainContext PluginDomainContext { private set; get; }

        /// <summary>
        /// 程序集的所在的位置
        /// </summary>
        public string AssemlbyLocation { private set; get; }


        /// <summary>
        /// 对应的应用域
        /// </summary>

        public AppDomain Domain { private set; get; }

        /// <summary>
        /// 是否安装过
        /// </summary>
        public bool Installed { private set; get; }

        public bool Debug { private set; get; }

        /// <summary>
        /// 插件域的对外服务
        /// </summary>
        public Dictionary<string, object> PluginDomainServices { private set; get; }

        #endregion

        #region 公有事件

        /// <summary>
        /// 安装成功的时候发生
        /// </summary>
        public event Action<PluginDomain> OnInstalled;

        /// <summary>
        /// 在关闭卸载的时候发生
        /// </summary>
        public event Action<PluginDomain> OnClosed;

        /// <summary>
        /// 发生异常的时候 发生
        /// </summary>

        public event Action<PluginDomain, PluginDomainException> OnException;

        #endregion

        public PluginDomain(Plugin plugin, PluginDomainFramework pluginDomainFramework)
        {
            this.Parameters = new Dictionary<string, string>();
            this.PluginDomainFramework = pluginDomainFramework;
            this.Installed = false;
            this.AssemlbyLocation = plugin.AssemblyLocation;
            this.Debug = plugin.IsDebug;

            this.Parameters.AddRange(plugin.Parameters);

        }

        #region 公有方法


        /// <summary>
        /// 安装
        /// </summary>
        public virtual void Install()
        {

            this.PluginDomainContext = new PluginDomainContext();
            this.PluginDomainContext.PluginDomainFramework = this.PluginDomainFramework;
            this.PluginDomainContext.Parameters.AddRange(this.Parameters);

            this.Domain = PluginDomainFactory.CreatePluginAppDomain(this.AssemlbyLocation, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.PluginDomainFramework.AddInsRoot));

            this.Domain.SetData("AssemlbyLocation", this.AssemlbyLocation);
            this.Domain.SetData("PluginDomainContext", this.PluginDomainContext);

            this.Domain.DoCallBack(PluginInitailizer.Start);

            this.PluginDomainName = this.Domain.GetData("PluginDomainName") as string;
            this.PluginDomainVersion = new Version(Convert.ToString(this.Domain.GetData("PluginDomainVersion")));
            this.PluginDomainServices = this.Domain.GetData("PluginDomainServcies") as Dictionary<string, object>;

            this.Installed = true;

            if (this.OnInstalled != null)
                this.OnInstalled(this);


        }


        /// <summary>
        /// 卸载
        /// </summary>
        public virtual void UnInstall()
        {
            if (this.PluginDomainServices != null)
                this.PluginDomainServices.Clear();

            if (this.Domain != null)
                AppDomain.Unload(this.Domain);

            this.Installed = false;

            if (this.OnClosed != null)
                this.OnClosed(this);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 插件域里面的服务都会经过里面，AOP思想
        /// </summary>
        /// <param name="target"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private object InvocationHandler(object target, MethodBase method, object[] parameters)
        {
            try
            {
                Console.WriteLine("Before: " + method.Name);
                object result = method.Invoke(target, parameters);
                Console.WriteLine("After: " + method.Name);
                return result;
            }
            catch (Exception ex)
            {
                if (OnException != null)
                    this.OnException(this, new PluginDomainException(ex.Message));
            }
            return null;
        }

        #endregion

    }
}
