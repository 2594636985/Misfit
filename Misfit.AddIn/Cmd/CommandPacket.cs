using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Cmd
{
    public class CommandPacket
    {
        public string CommandName { set; get; }

        public Dictionary<string, string> Parameters { set; get; }
    }
}
