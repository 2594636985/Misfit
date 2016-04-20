using System;
using Misfit.AddIn;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Commands
{
    [Command("stop", "Stop a bundle by Id.", MinArgs = 1, UsageSummary = "<Id> [options]", UsageDescription = "Stop a bundle by Id.")]
    public class StopCommand : Command
    {
        public override void Execute()
        {
            if (Arguments.Count > 0)
            {
                try
                {
                    shell = ConsoleAddIn.Shell;
                    int id = Convert.ToInt32(Arguments[0]);
                    IBundle bundle = shell.Framework.Bundles.GetBundle(id);
                    shell.Framework.StopBundle(id);
                    System.Console.WriteLine("Bundle: {0} stoped.[#{1}]", bundle.SymbolicName, bundle.Id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
