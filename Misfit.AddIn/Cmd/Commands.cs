using Misfit.AddIn.Pipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Cmd
{
    public class Commands
    {
        public static object Execute()
        {
            using (NamedPipeClient namedPipeClient = new NamedPipeClient())
            {
                namedPipeClient.Connect();
                namedPipeClient.WriteLine("Exit");
                namedPipeClient.ReadLine();
            }

            return null;
        }
    }
}
