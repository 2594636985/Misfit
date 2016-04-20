using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Misfit.AddIn;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Commands
{
    [Command("install", "Install a OSGi bundle to the framework.", MinArgs = 1, MaxArgs = 1, UsageSummary = "<path...> [options]", UsageDescription = "Install a OSGi bundle.")]
    public class InstallCommand : Command
    {
        private IBootstrapper shell = null;

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
                    System.Console.WriteLine("Bundle: {0} installed.[#{1}]", bundle.SymbolicName, bundle.Id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
