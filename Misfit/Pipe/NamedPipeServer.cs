using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO.Pipes;

namespace Misfit.Pipe
{
    class NamedPipeServer
    {
        private const string PipeServerName = "MisfitPipe";
        private BackgroundWorker _serverBackgroundWorker;
        private readonly List<NamedPipeConnection> _connections = new List<NamedPipeConnection>();
        private int _nextPipeId;
        private volatile bool _keepRunning = true;
        public NamedPipeServer()
        {

        }

        public void Start()
        {
            if (this._serverBackgroundWorker == null)
            {
                this._serverBackgroundWorker = new BackgroundWorker();
                this._serverBackgroundWorker.WorkerSupportsCancellation = true;
                this._serverBackgroundWorker.DoWork += ServerBackgroundWorker_DoWork;
                this._serverBackgroundWorker.RunWorkerAsync();
            }
        }

        #region 私有方法
        private void ServerBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (this._keepRunning)
            {
                NamedPipeServerStream acceptPipe = null;
                NamedPipeServerStream dataPipe = null;
                NamedPipeConnection connection = null;

                var connectionPipeName = GetNextConnectionPipeName(PipeServerName);

                try
                {
                    acceptPipe = PipeServerFactory.CreateAndConnectPipe(PipeServerName);
                    var acceptWrapper = new PipeStreamWrapper(acceptPipe);
                    acceptWrapper.WriteLine(connectionPipeName);
                    acceptWrapper.WaitForPipeDrain();
                    acceptWrapper.Close();

                    dataPipe = PipeServerFactory.CreatePipe(connectionPipeName);
                    dataPipe.WaitForConnection();
                    connection = ConnectionFactory.CreateConnection(dataPipe);
                    connection.Open();

                    lock (_connections)
                    {
                        _connections.Add(connection);
                    }

                    ClientOnConnected(connection);
                }
                // Catch the IOException that is raised if the pipe is broken or disconnected.
                catch (Exception e)
                {
                    Console.Error.WriteLine("Named pipe is broken or disconnected: {0}", e);

                    Cleanup(acceptPipe);
                    Cleanup(dataPipe);

                    ClientOnDisconnected(connection);
                }
            }

        }

        /// <summary>
        /// 获得下一个连接的通道的名称
        /// </summary>
        /// <param name="pipeName"></param>
        /// <returns></returns>
        private string GetNextConnectionPipeName(string pipeName)
        {
            return string.Format("{0}_{1}", pipeName, ++_nextPipeId);
        }

        #endregion

    }
}
