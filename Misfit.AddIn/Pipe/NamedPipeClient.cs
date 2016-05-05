using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace Misfit.AddIn.Pipe
{
    public class NamedPipeClient : IDisposable
    {
        private readonly string PipeClentName = "MisfitNamedPipe";
        private NamedPipeClientStream _namedPipeClientStream;
        private NamedPipeConnection _connection;

        public void Close()
        {
            if (_connection != null)
                _connection.Close();

            if (this._namedPipeClientStream != null && !this._namedPipeClientStream.IsConnected)
            {
                this._namedPipeClientStream.Close();
                this._namedPipeClientStream.Dispose();
            }
        }

        public void Connect()
        {
            NamedPipeClientStream namedPipeConnectServer = PipeClientFactory.CreateClientPipe(PipeClentName);
            NamedPipeConnection namedPipeConnectionServer = new NamedPipeConnection(namedPipeConnectServer);
            string dataPipeName = namedPipeConnectionServer.ReadLine();
            namedPipeConnectionServer.Close();

            this._namedPipeClientStream = PipeClientFactory.CreateClientPipe(dataPipeName);
            this._connection = new NamedPipeConnection(this._namedPipeClientStream);
        }

        public void WriteLine(string message)
        {
            this._connection.WriteLine(message);
        }

        public string ReadLine()
        {
            return this._connection.ReadLine();
        }

        public void Dispose()
        {
            this.Close();
        }
    }
}
