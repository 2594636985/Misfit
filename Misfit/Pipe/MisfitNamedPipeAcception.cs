using Misfit.AddIn.Pipe;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Misfit.Pipe
{
    /// <summary>
    /// 用于接受子AppDomain来的信息类
    /// </summary>
    public class MisfitNamedPipeAcception
    {
        private CancellationTokenSource _cancellationTokenSource;
        private NamedPipeServerStream _namedPipeServerStream;
        private NamedPipeConnection _namedPipeConnection;
        private Task _acceptMessageTask;

        public string Name { private set; get; }

        public event Action<NamedPipeConnection, string> OnAcceptMessage;

        public event Action<MisfitNamedPipeAcception, NamedPipeConnection> OnClosed;

        public MisfitNamedPipeAcception(string name, NamedPipeServerStream namedPipeServerStream)
        {
            this.Name = name;
            this._namedPipeServerStream = namedPipeServerStream;
            this._namedPipeConnection = new NamedPipeConnection(namedPipeServerStream);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Listener()
        {
            this._cancellationTokenSource = new CancellationTokenSource();
            this._acceptMessageTask = new Task(this.DoReadMessage, null, this._cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
            this._acceptMessageTask.Start();
        }

        /// <summary>
        /// 读取信息
        /// </summary>
        /// <param name="argument"></param>
        private void DoReadMessage(object argument)
        {
            while (this._namedPipeConnection.IsConnected && this._namedPipeConnection.CanRead)
            {
                string message = this._namedPipeConnection.ReadLine();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    if (this.OnAcceptMessage != null)
                        this.OnAcceptMessage(this._namedPipeConnection, message);
                }
            }
        }
    }
}
