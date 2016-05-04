using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;

namespace Misfit.Pipe
{
    class ConnectionFactory
    {
        private static int _lastId;

        public static NamedPipeConnection CreateConnection(PipeStream pipeStream)
        {
            return new NamedPipeConnection(++_lastId, "Client " + _lastId, pipeStream);
        }
    }
}
