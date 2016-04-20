using System;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Commands
{
    [Command("clear", "Clear the console.", AltName = "cls", MaxArgs = 0, UsageSummary = "", UsageDescription = "Clear the console.")]
    public class ClearCommand : Command
    {
        public override void Execute()
        {
            System.Console.Clear();
        }
    }
}
