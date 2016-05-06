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

        public ICommand FindCommand(string commandName)
        {
            if (this.ContainsKey(commandName))
                return this[commandName];
            return null;
        }
    }
}
