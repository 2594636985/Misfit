using Misfit.AddIn;
using Misfit.AddIn.Cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Cmd
{
    public class ExitCommand : AbstractCommand
    {
        public ExitCommand(IPluginFramework pluginFramework)
            : base(pluginFramework)
        { }

        public override string CommandName
        {
            get
            {
                return "Exit";
            }
        }

        public override object Execute()
        {
            return null;
        }
    }
}
