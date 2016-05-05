using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Misfit.AddIn.Pipe
{
    public class NamedPipeConnection
    {
        private const int HeaderSize = sizeof(int);
        private PipeStream _pipeStream;

        public PipeStream PipeStream
        {
            get { return this._pipeStream; }
        }


        public bool IsConnected
        {
            get { return PipeStream.IsConnected; }
        }

        public bool CanRead
        {
            get { return PipeStream.CanRead; }
        }

        public bool CanWrite
        {
            get { return PipeStream.CanWrite; }
        }


        public event Action<Exception> OnException;

        public NamedPipeConnection(PipeStream pipeStream)
        {
            this._pipeStream = pipeStream;
        }


        #region 私有方法

        /// <summary>
        /// 写入信息
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            if (IsConnected && this._pipeStream.CanWrite)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] lenbuf = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(data.Length));
                this._pipeStream.Write(lenbuf, 0, lenbuf.Length);
                this._pipeStream.Write(data, 0, data.Length);
                this._pipeStream.Flush();
                this._pipeStream.WaitForPipeDrain();
            }
        }

        public string ReadLine()
        {
            try
            {
                var lenbuf = new byte[HeaderSize];
                var bytesRead = this._pipeStream.Read(lenbuf, 0, HeaderSize);
                if (bytesRead != 0 && bytesRead == HeaderSize)
                {
                    int len = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lenbuf, 0));
                    if (len == 0)
                        return string.Empty;

                    byte[] data = new byte[len];
                    this._pipeStream.Read(data, 0, len);

                    return Encoding.UTF8.GetString(data);
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                if (this.OnException != null)
                    this.OnException(e);
            }
            return string.Empty;
        }

        public void Close()
        {
            if (this._pipeStream != null)
            {
                this._pipeStream.Close();
                this._pipeStream.Dispose();
            }
        }

        #endregion
    }
}
