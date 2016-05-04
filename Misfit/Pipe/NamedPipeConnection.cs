using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Misfit.Pipe
{
    public class NamedPipeConnection
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly PipeStreamWrapper _streamWrapper;
        /// <summary>
        /// 连接ID
        /// </summary>
        public int Id { private set; get; }
        /// <summary>
        /// 连接名字
        /// </summary>
        public string Name { private set; get; }

        public bool IsConnected
        {
            get
            {
                return _streamWrapper.IsConnected;
            }
        }

        public event Action<Exception> OnException;

        public event Action<string> OnAcceptMessage;

        public NamedPipeConnection(int id, string name, PipeStream serverStream)
        {
            Id = id;
            Name = name;
            _streamWrapper = new PipeStreamWrapper(serverStream);
        }

        public void Open()
        {
            this._cancellationTokenSource = new CancellationTokenSource();
            new Task(DoWorkImpl, null, this._cancellationTokenSource.Token, TaskCreationOptions.LongRunning).Start();
        }

        public void Close()
        {
            _streamWrapper.Close();

            if (this._cancellationTokenSource != null && !this._cancellationTokenSource.IsCancellationRequested)
            {
                this._cancellationTokenSource.Cancel();
                this._cancellationTokenSource.Dispose();
            }
        }

        #region 私有方法

        private void DoWorkImpl(object argument)
        {
            try
            {
                while (IsConnected && _streamWrapper.CanRead)
                {
                    string wordline = _streamWrapper.ReadLine();
                    if (!string.IsNullOrWhiteSpace(wordline))
                    {

                    }
                }
            }
            catch (Exception e)
            {
                if (this.OnException != null)
                    this.OnException(e);

            }
        }

        /// <summary>
        /// 写入信息
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            if (IsConnected && _streamWrapper.CanWrite)
            {
                _streamWrapper.WriteLine(message);
                _streamWrapper.WaitForPipeDrain();
            }
        }
        #endregion
    }
}
