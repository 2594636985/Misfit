using Misfit.AddIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Cmd
{
    public class ActivatePluginCommand : AbstractCommand
    {
        public ActivatePluginCommand(IPluginFramework pluginFramework)
            : base(pluginFramework)
        { }

        public override string CommandName
        {
            get
            {
                return "ActivatePlugin";
            }
        }

        public override object Execute(Dictionary<string, string> parameters)
        {
            return null;
        }
    }
}
