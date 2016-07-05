using Misfit.Modulation.AddIn;
using Misfit.Modulation.AddIn.Core;
using Misfit.Modulation.AddIn.Injection;
using Misfit.Modulation.AddIn.IO;
using Misfit.Modulation.AddIn.Serices;
using Misfit.Modulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Misfit.Modulation.Aspect;
using Misfit.Modulation.AddIn.Tracking;
using Misfit.Modulation.Tracking;

namespace Misfit.Modulation
{
    /// <summary>
    /// 模块域
    /// </summary>
    public class ModuleDomain : MarshalByRefObject
    {
        #region 公有成员
        public Tracker Tracker { private set; get; }

        /// <summary>
        /// 模块域的哉名
        /// </summary>
        public string DomainName { private set; get; }

        /// <summary>
        /// 模块域的名称
        /// </summary>
        public string ModuleDomainName { private set; get; }

        /// <summary>
        /// 模块域的版本号
        /// </summary>
        public Version ModuleDomainVersion { private set; get; }

        /// <summary>
        /// 模块域上下文
        /// </summary>
        public ModuleDomainContext ModuleDomainContext { private set; get; }

        /// <summary>
        /// 对应的应用域
        /// </summary>

        public AppDomain Domain { private set; get; }

        /// <summary>
        /// 是否安装过
        /// </summary>
        public bool Installed { private set; get; }

        /// <summary>
        /// 模块域的对外服务
        /// </summary>
        public Dictionary<string, object> ModuleDomainServices { private set; get; }
        #endregion

        #region 公有事件
        /// <summary>
        /// 初始化的时候发生
        /// </summary>
        public event Action<ModuleDomain> OnInitialized;

        /// <summary>
        /// 安装成功的时候发生
        /// </summary>
        public event Action<ModuleDomain> OnInstalled;

        /// <summary>
        /// 在关闭卸载的时候发生
        /// </summary>
        public event Action<ModuleDomain> OnClosed;

        /// <summary>
        /// 发生异常的时候 发生
        /// </summary>

        public event Action<ModuleDomain, ModuleException> OnException;

        #endregion
        public ModuleDomain(ModuleDomainContext moduleDomainContext)
        {
            this.ModuleDomainContext = moduleDomainContext;
            this.DomainName = moduleDomainContext.ModuleDomainName;
            this.ModuleDomainServices = new Dictionary<string, object>();
            this.Installed = false;
            this.Tracker = new Tracker(this.DomainName);

            if (moduleDomainContext.IsDebug)
            {
                if (moduleDomainContext.TrackerTarget == TrackerTarget.File)
                    this.Tracker.Target = new FileTarget();
                else if (moduleDomainContext.TrackerTarget == TrackerTarget.Console)
                    this.Tracker.Target = new ConsoleTarget();
                else
                    this.Tracker.Target = new DebugTarget();
            }
            else
            {
                this.Tracker.Target = new NullTarget();
            }

        }

        #region 公有方法

        /// <summary>
        /// 初始化模块域
        /// </summary>
        public void Initialize()
        {
            this.Tracker.Info(string.Format("开始初始化{0}的模块域", this.DomainName));

            this.Domain = ModuleDomainFactory.CreateModuleAppDomain("Module-" + this.ModuleDomainContext.AssemlbyLocation, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.ModuleDomainContext.AddInsRoot));

            this.Domain.SetData("ModuleDomainTracker", this.Tracker);
            this.Domain.SetData("Location", this.ModuleDomainContext.AssemlbyLocation);
            this.Domain.SetData("ModuleDomainContext", this.ModuleDomainContext);

            this.Domain.DoCallBack(ModuleDomainInitailize.Initailize);

            this.ModuleDomainName = this.Domain.GetData("ModuleDomainName") as string;
            this.ModuleDomainVersion = new Version(Convert.ToString(this.Domain.GetData("ModuleDomainVersion")));

            if (this.OnInitialized != null)
                this.OnInitialized(this);

            this.Tracker.Info(string.Format("结束{0}模块域初始化", this.DomainName));
        }


        /// <summary>
        /// 安装
        /// </summary>
        public void Install()
        {
            this.Tracker.Info(string.Format("开始安装{0}模块域的服务", this.DomainName));

            this.Domain.DoCallBack(ModuleDomainInitailize.Start);

            Dictionary<string, object> registerModuleDoaminServices = this.Domain.GetData("ModuleDomainServcies") as Dictionary<string, object>;

            if (registerModuleDoaminServices != null && registerModuleDoaminServices.Count > 0)
            {
                foreach (string registerKey in registerModuleDoaminServices.Keys)
                {
                    object domainService = DynamicProxyFactory.Instance.CreateProxy(registerModuleDoaminServices[registerKey], new InvocationDelegate(InvocationHandler), true);
                    this.ModuleDomainServices.Add(registerKey, domainService);
                }
            }

            this.Installed = true;

            if (this.OnInstalled != null)
                this.OnInstalled(this);

            this.Tracker.Info(string.Format("结束安装{0}模块域的服务", this.DomainName));

        }


        /// <summary>
        /// 卸载
        /// </summary>
        public void UnInstall()
        {
            if (this.ModuleDomainServices != null)
                this.ModuleDomainServices.Clear();

            if (this.Domain != null)
                AppDomain.Unload(this.Domain);

            this.Installed = false;

            if (this.OnClosed != null)
                this.OnClosed(this);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 模块域里面的服务都会经过里面，AOP思想
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
                    this.OnException(this, new ModuleException(ex.Message));
            }
            return null;
        }

        #endregion

    }
}
