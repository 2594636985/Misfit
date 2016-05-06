using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Cmd
{
    public class CommandResult
    {
        public bool Status { set; get; }

        public string ErrorMessage { set; get; }

        public string Body { set; get; }
    }
}
