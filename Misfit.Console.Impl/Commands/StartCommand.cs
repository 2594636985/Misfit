using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Misfit.AddIn;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Commands
{
    [Command("start", "Start a bundle by Id.", MinArgs = 1, UsageSummary = "<Id> [options]", UsageDescription = "Start a bundle by Id.")]
    public class StartCommand : Command
    {
        private IBootstrapper shell = null;

        public override void Execute()
        {
            if (Arguments.Count > 0)
            {
                try
                {
                    shell = ConsoleAddIn.Shell;
                    int id = Convert.ToInt32(Arguments[0]);
                    IBundle bundle = shell.Framework.StartBundle(id);
                    System.Console.WriteLine("Bundle: {0} started.[#{1}]", bundle.SymbolicName, bundle.Id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
