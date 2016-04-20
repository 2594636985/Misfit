using System;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Commands
{
    [Command("exit", "Exit the framework.", MaxArgs = 0, UsageSummary = "", UsageDescription = "Exit the framework.")]
    public class ExitCommand : Command
    {
        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}
