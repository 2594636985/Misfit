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

    public class PipeStreamReader
    {
        public PipeStream BaseStream { get; private set; }
        public bool IsConnected { get; private set; }

        private readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();
        public PipeStreamReader(PipeStream stream)
        {
            BaseStream = stream;
            IsConnected = stream.IsConnected;
        }
        private int ReadLength()
        {
            const int lensize = sizeof(int);
            var lenbuf = new byte[lensize];
            var bytesRead = BaseStream.Read(lenbuf, 0, lensize);
            if (bytesRead == 0)
            {
                IsConnected = false;
                return 0;
            }
            if (bytesRead != lensize)
                throw new IOException(string.Format("Expected {0} bytes but read {1}", lensize, bytesRead));
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lenbuf, 0));
        }

        private string ReadObject(int len)
        {
            var data = new byte[len];
            BaseStream.Read(data, 0, len);
            using (var memoryStream = new MemoryStream(data))
            {
                return (string)_binaryFormatter.Deserialize(memoryStream);
            }
        }



        public string ReadObject()
        {
            var len = ReadLength();
            return len == 0 ? default(string) : ReadObject(len);
        }
    }
}
