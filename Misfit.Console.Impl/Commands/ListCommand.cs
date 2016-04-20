using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn;
using Misfit.Console.CommandLineParser;
using Misfit.Console.Views;

namespace Misfit.Console.Commands
{
    [Command("list", "List the bundles of the framework.", AltName = "l", MaxArgs = 1, UsageSummary = "[filter]", UsageDescription = "List the bundles of the framework.")]
    public class ListCommand : Command
    {
        private IBootstrapper shell = null;
        private ListConsoleView view;

        public override void Execute()
        {
            view = new ListConsoleView();

            shell = ConsoleAddIn.Shell;
            view.ViewList(shell);
        }
    }
}
