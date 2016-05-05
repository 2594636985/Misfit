using Misfit.AddIn.Cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Cmd
{
    public class CommandCollection : Dictionary<string, ICommand>
    {
        public void Add(ICommand command)
        {
            this.Add(command.CommandName, command);
        }
    }
}
