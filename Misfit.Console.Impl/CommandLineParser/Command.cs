using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Console.CommandLineParser
{
    public abstract class Command : ICommand
    {
        private CommandLineHelper commandLineHelper;
        private List<string> arguments = new List<string>();

        public Command()
        {

        }

        #region ICommand Members

        public CommandLineHelper Helper
        {
            get
            {
                return commandLineHelper;
            }
            set
            {
                commandLineHelper = value;
            }
        }

        public List<string> Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }

        public abstract void Execute();

        #endregion
    }
}
