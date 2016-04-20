using System;
using System.Diagnostics;
using Misfit.AddIn;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Commands
{
    [Command("launch", "Launch a OSGi bundle.", MinArgs = 1, MaxArgs = 1, UsageSummary = "<path...> [options]", UsageDescription = "Install & start a OSGi bundle.")]
    public class LaunchCommand : Command
    {
        IBootstrapper shell = null;

        public override void Execute()
        {
            if (Arguments.Count > 0)
            {
                try
                {
                    shell = ConsoleAddIn.Shell;
                    Debug.Assert(shell != null);

                    string bundleLocation = Arguments[0];
                    IBundle bundle = shell.Framework.InstallBundle(bundleLocation);
                    shell.Framework.StartBundle(bundle);
                    System.Console.WriteLine("Bundle: {0} launched(installed & started).[#{1}]", bundle.SymbolicName, bundle.Id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
