using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Pipe
{
    public class PipeClientFactory
    {
        public static NamedPipeClientStream CreateClientPipe(string pipeName)
        {
            var pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.WriteThrough);
            pipe.Connect();
            return pipe;
        }
    }
}
