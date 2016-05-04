using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;

namespace Misfit.Pipe
{
    class PipeStreamWrapper
    {
        private readonly PipeStreamReader _reader;
        private readonly PipeStreamWriter _writer;

        public PipeStream BaseStream { get; private set; }
        public bool IsConnected
        {
            get { return BaseStream.IsConnected && _reader.IsConnected; }
        }

        public bool CanRead
        {
            get { return BaseStream.CanRead; }
        }
        public bool CanWrite
        {
            get { return BaseStream.CanWrite; }
        }

       
        public PipeStreamWrapper(PipeStream stream)
        {
            BaseStream = stream;
            _reader = new PipeStreamReader(BaseStream);
            _writer = new PipeStreamWriter(BaseStream);
        }
        public string ReadLine()
        {
            return _reader.ReadObject();
        }

        public void WriteLine(string obj)
        {
            _writer.WriteLine(obj);
        }


        public void WaitForPipeDrain()
        {
            _writer.WaitForPipeDrain();
        }


        public void Close()
        {
            BaseStream.Close();
        }
    }
}
