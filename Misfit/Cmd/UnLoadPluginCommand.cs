using Misfit.AddIn;
using Misfit.AddIn.Cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Cmd
{
    public class UnLoadPluginCommand : AbstractCommand
    {
        public UnLoadPluginCommand(IPluginFramework pluginFramework)
            : base(pluginFramework)
        { }

        public override string CommandName
        {
            get { return "UnLoadPlugin"; }
        }

        public override object Execute()
        {
            throw new NotImplementedException();
        }
    }
}
