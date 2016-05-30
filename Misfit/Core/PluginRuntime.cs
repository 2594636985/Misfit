using Misfit.AddIn;
using Misfit.Pipe;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Misfit.Core
{
    public class PluginRuntime
    {
        private int _nextPipeId;
        private volatile bool _keepRunning = true;

        public CancellationTokenSource CancellationTokenSource { private set; get; }
        public Task Task { private set; get; }
        /// <summary>
        /// 所属于应用域
        /// </summary>
        public AppDomain Domain { private set; get; }

        /// <summary>
        /// 插件对应的程序集
        /// </summary>
        public Assembly Assembly { private set; get; }

        /// <summary>
        /// 运行插件
        /// </summary>
        public IPlugin Plugin { private set; get; }

        /// <summary>
        /// 插件调动者类
        /// </summary>
        public IPluginActivator PluginAcitvator { private set; get; }

        /// <summary>
        /// 关闭事件
        /// </summary>
        public event Action<PluginRuntime, AppDomain> Closed;

        public event Action<PluginRuntime, Exception> OnUnhandledException;

        public PluginRuntime(IPlugin plugin)
        {
            this.Plugin = plugin;
        }

        #region 私有方法


        /// <summary>
        /// 未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Domain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }

        private static void Domain_DomainUnload(object sender, EventArgs e)
        {

        }

        private static void DomainStartDoCallBack()
        {
            ThreadStart start = new ThreadStart(() =>
            {
                AppDomain pluginDomain = Thread.GetDomain();
                string pluginLocation = Convert.ToString(pluginDomain.GetData("PluginLoaction"));
                Assembly pluginAssembly = pluginDomain.Load(pluginLocation);

                Type pluginActivatorType = pluginAssembly.GetExportedTypes()
                    .FirstOrDefault(t => t.IsClass && t.GetInterfaces().Count(inf => inf.IsAssignableFrom(typeof(IPluginActivator))) > 0);

                if (pluginActivatorType == null)
                    throw new PluginException("没有找到对应的激活类");

                IPluginActivator pluginAcitvator = (IPluginActivator)pluginDomain.CreateInstanceAndUnwrap(pluginLocation, pluginActivatorType.FullName);
                pluginAcitvator.Start(null);

            });

            Thread uiThread = new Thread(start);
            uiThread.SetApartmentState(ApartmentState.STA);
            uiThread.IsBackground = false;
            uiThread.Start();
        }

        /// <summary>
        /// 如果加载失败之后，去搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AssemblyName name = new AssemblyName(args.Name);
            string assemblyFile = SearchAssembly(name.Name);
            return Assembly.LoadFrom(assemblyFile);
        }

        /// <summary>
        /// 搜索对应的程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static string SearchAssembly(string assemblyName)
        {
            string appRoot = AppDomain.CurrentDomain.BaseDirectory;
            string addInsRoot = Path.Combine(appRoot, Constants.AddInsFileRoot);

            {
                string[] files = Directory.GetFiles(appRoot,
                    assemblyName + ".dll", SearchOption.TopDirectoryOnly);

                if (files != null && files.Length > 0)
                {
                    return files[0];
                }
            }

            {
                string[] files = Directory.GetFiles(addInsRoot,
                    assemblyName + ".dll", SearchOption.TopDirectoryOnly);

                if (files != null && files.Length > 0)
                {
                    return files[0];
                }
            }

            return string.Empty;
        }

        private void DoStart(object state)
        {
            this.Domain = this.Plugin.PluginFramework.CreateDomain("Plugin-" + this.Plugin.Location);
            this.Domain.DomainUnload += Domain_DomainUnload;
            this.Domain.UnhandledException += Domain_UnhandledException;
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
            //this.PluginAcitvator = (IPluginActivator)this.Domain.CreateInstanceAndUnwrap(this.Plugin.Location, this.Plugin.Activator);

            this.Domain.ExecuteAssemblyByName("PluginShell", this.Plugin.Location, this.Plugin.Activator);

            //if (this.PluginAcitvator != null)
            //    this.PluginAcitvator.Start(null);
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 开始运行插件
        /// </summary>
        public void Start()
        {
            try
            {
                this.CancellationTokenSource = new CancellationTokenSource();
                this.Task = new Task(this.DoStart, null, this.CancellationTokenSource.Token, TaskCreationOptions.LongRunning);
                this.Task.Start();
            }
            catch (Exception ex)
            {
                throw new PluginException(ex.Message, ex);
            }
        }



        /// <summary>
        /// 停止插件
        /// </summary>
        public void Stop()
        {
            try
            {
                if (this.Domain != null)
                {
                    //if (this.PluginAcitvator != null)
                    //    this.PluginAcitvator.Stop(null);

                    AppDomain.Unload(this.Domain);

                    if (this.CancellationTokenSource != null && !this.CancellationTokenSource.IsCancellationRequested)
                    {
                        this.CancellationTokenSource.Cancel();
                        this.CancellationTokenSource.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new PluginException(ex.Message, ex);
            }
        }

        #endregion
    }
}
