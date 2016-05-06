using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO.Pipes;
using Misfit.AddIn.Pipe;

namespace Misfit.Pipe
{
    public class MisfitNamedPipeServer
    {
        private readonly string PipeServerName = "MisfitNamedPipe";
        private BackgroundWorker _serverBackgroundWorker;
        private MisfitNamedPipe _aomiNamedPipe;
        private MisfitNamedPipeAcceptionCollection _misfitNamedPipeAcceptionCollection = new MisfitNamedPipeAcceptionCollection();
        private int _nextPipeId;
        private volatile bool _keepRunning = true;

        /// <summary>
        ///接受到信息
        /// </summary>
        public event Action<NamedPipeConnection, string> OnAcceptMessage;

        public void Start()
        {
            if (this._serverBackgroundWorker == null)
            {
                if (this._aomiNamedPipe == null)
                    this._aomiNamedPipe = new MisfitNamedPipe(PipeServerName);

                this._serverBackgroundWorker = new BackgroundWorker();
                this._serverBackgroundWorker.WorkerSupportsCancellation = true;
                this._serverBackgroundWorker.DoWork += ServerBackgroundWorker_DoWork;
                this._serverBackgroundWorker.RunWorkerAsync();
            }


        }

        public void Close()
        {
            if (this._keepRunning)
            {
                this._keepRunning = false;

                NamedPipeClientStream namedPipeConnectServer = PipeClientFactory.CreateClientPipe(PipeServerName);
                NamedPipeConnection namedPipeConnectionServer = new NamedPipeConnection(namedPipeConnectServer);
                string dataPipeName = namedPipeConnectionServer.ReadLine();
                namedPipeConnectionServer.Close();

                NamedPipeClientStream namedPipeClientStream = PipeClientFactory.CreateClientPipe(dataPipeName);
                NamedPipeConnection connection = new NamedPipeConnection(namedPipeClientStream);
                connection.WriteLine(" ");
                connection.Close();
            }
        }

        #region 私有方法

        private void ServerBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (this._keepRunning)
            {
                MisfitNamedPipeAcception misfitNamedPipeAcception = this._aomiNamedPipe.Accepted();
                misfitNamedPipeAcception.OnClosed += MisfitNamedPipeAcception_OnClosed;
                misfitNamedPipeAcception.OnAcceptMessage += MisfitNamedPipeAcception_OnAcceptMessage;
                misfitNamedPipeAcception.Listener();

                this._misfitNamedPipeAcceptionCollection.Add(misfitNamedPipeAcception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acception"></param>
        /// <param name="connection"></param>
        private void MisfitNamedPipeAcception_OnClosed(MisfitNamedPipeAcception acception, NamedPipeConnection connection)
        {
            if (this._misfitNamedPipeAcceptionCollection.ContainsKey(acception.Name))
            {
                this._misfitNamedPipeAcceptionCollection.Remove(acception.Name);
            }
        }

        /// <summary>
        /// 接受到信息
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="message"></param>
        private void MisfitNamedPipeAcception_OnAcceptMessage(NamedPipeConnection connection, string message)
        {
            if (this.OnAcceptMessage != null)
                this.OnAcceptMessage(connection, message);
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
