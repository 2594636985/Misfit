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

namespace Misfit.Core
{
    public class PluginRuntime
    {
        private BackgroundWorker _pipeBackgroundWorker;
        private readonly List<NamedPipeConnection> _connections = new List<NamedPipeConnection>();
        private int _nextPipeId;
        private volatile bool _keepRunning = true;
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
        public IPluginActivator Acitvator { private set; get; }

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
        /// 运行通信管道上的服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PipeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (this._keepRunning)
            {
                NamedPipeServerStream acceptPipe = null;
                NamedPipeServerStream dataPipe = null;
                NamedPipeConnection connection = null;

                var connectionPipeName = string.Format("{0}_{1}", this.Plugin.Name, ++_nextPipeId);

                try
                {
                    acceptPipe = PipeServerFactory.CreateAndConnectPipe(this.Plugin.Name);
                    var acceptWrapper = new PipeStreamWrapper(acceptPipe);
                    acceptWrapper.WriteLine(connectionPipeName);
                    acceptWrapper.WaitForPipeDrain();
                    acceptWrapper.Close();

                    dataPipe = PipeServerFactory.CreatePipe(connectionPipeName);
                    dataPipe.WaitForConnection();

                    connection = ConnectionFactory.CreateConnection(dataPipe);
                    connection.OnAcceptMessage += Connection_OnAcceptMessage;
                    connection.Open();

                    lock (_connections)
                    {
                        _connections.Add(connection);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Named pipe is broken or disconnected: {0}", ex);

                    if (acceptPipe != null)
                    {
                        acceptPipe.Close();
                        acceptPipe.Dispose();
                    }
                    if (dataPipe != null)
                    {
                        dataPipe.Close();
                        dataPipe.Dispose();
                    }
                }
            }
        }

        private void Connection_OnAcceptMessage(string message)
        {

        }

        /// <summary>
        /// 读取副APPDOMAIN来的信息
        /// </summary>
        /// <param name="obj"></param>
        private void NamedPipeConnection_OnReadLine(string message)
        {

        }


        /// <summary>
        /// 通信管道上的异常
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>

        private void NamedPipeConnection_OnException(Exception e)
        {
            if (this.OnUnhandledException != null)
                this.OnUnhandledException(this, e);
        }

        /// <summary>
        /// 未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Domain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (this.OnUnhandledException != null)
                this.OnUnhandledException(this, e.ExceptionObject as Exception);
        }

        private static void Domain_DomainUnload(object sender, EventArgs e)
        {

        }

        private static void DomainDoCallBack()
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
        #endregion

        #region 公有方法

        /// <summary>
        /// 开始运行插件
        /// </summary>
        public void Start()
        {
            try
            {
                this._pipeBackgroundWorker = new BackgroundWorker();
                this._pipeBackgroundWorker.WorkerSupportsCancellation = true;
                this._pipeBackgroundWorker.DoWork += PipeBackgroundWorker_DoWork;
                this._pipeBackgroundWorker.RunWorkerAsync();

                this.Domain = this.Plugin.PluginFramework.CreateDomain("Plugin-" + this.Plugin.Location);
                this.Domain.SetData("PluginLoaction", this.Plugin.Location);
                this.Domain.DomainUnload += Domain_DomainUnload;
                this.Domain.UnhandledException += Domain_UnhandledException;
                this.Domain.DoCallBack(DomainDoCallBack);
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
                    if (this.Acitvator != null)
                        this.Acitvator.Stop(null);

                    AppDomain.Unload(this.Domain);
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
