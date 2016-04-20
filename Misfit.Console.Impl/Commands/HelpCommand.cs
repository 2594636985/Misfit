using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.Console.CommandLineParser;
using Misfit.Console.Views;

namespace Misfit.Console.Commands
{
    [Command("help", "Describe the usage of this program and its commands.", MaxArgs = 1, AltName = "?", UsageSummary = "<command>", UsageDescription = "Describe the usage of this program and its commands.")]
    public class HelpCommand : Command
    {
        private HelpConsoleView view;

        public HelpCommand()
        {
            
        }

        public override void Execute()
        {
            view = new HelpConsoleView(this.Helper, "Egeye console",
                                       "http://www.codeplex.com/EgeyeAddIn");
            if (Arguments.Count == 0)
                view.ViewHelp();
            else
                view.ViewHelpForCommand(Arguments[0]);
        }
    }
}
