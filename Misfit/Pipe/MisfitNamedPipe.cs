using Misfit.AddIn.Pipe;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;

namespace Misfit.Pipe
{
    public class MisfitNamedPipe
    {
        private int _nextPipeId;
        public string PipeName { private set; get; }
        public MisfitNamedPipe(string pipeName)
        {
            this.PipeName = pipeName;
        }

        /// <summary>
        /// 接受到客户的一个数据联接
        /// </summary>
        /// <returns></returns>
        public MisfitNamedPipeAcception Accepted()
        {
            var connectionPipeName = GetNextConnectionPipeName(this.PipeName);

            NamedPipeServerStream acceptNamedPipe = MisfitPipeServerFactory.CreateAndConnectPipe(this.PipeName);
            NamedPipeConnection acceptNamedPipeConnection = new NamedPipeConnection(acceptNamedPipe);
            acceptNamedPipeConnection.WriteLine(connectionPipeName);
            acceptNamedPipeConnection.Close();

            NamedPipeServerStream dataNamedPipe = MisfitPipeServerFactory.CreatePipe(connectionPipeName);
            dataNamedPipe.WaitForConnection();

            return new MisfitNamedPipeAcception(connectionPipeName, dataNamedPipe);
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
    }
}
