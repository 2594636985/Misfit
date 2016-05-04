using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Misfit.Pipe
{
    public class PipeStreamWriter
    {
        public PipeStream BaseStream { get; private set; }

        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        public PipeStreamWriter(PipeStream stream)
        {
            BaseStream = stream;
        }
        private byte[] Serialize(string value)
        {
            using (var memoryStream = new MemoryStream())
            {
                _binaryFormatter.Serialize(memoryStream, value);
                return memoryStream.ToArray();
            }
        }

        private void WriteLength(int len)
        {
            var lenbuf = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(len));
            BaseStream.Write(lenbuf, 0, lenbuf.Length);
        }

        private void Write(byte[] data)
        {
            BaseStream.Write(data, 0, data.Length);
        }

        private void Flush()
        {
            BaseStream.Flush();
        }

        public void WriteLine(string obj)
        {
            var data = Serialize(obj);
            WriteLength(data.Length);
            Write(data);
            Flush();
        }

        public void WaitForPipeDrain()
        {
            BaseStream.WaitForPipeDrain();
        }
    }
}