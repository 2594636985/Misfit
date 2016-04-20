using System;
using Misfit.Console.CommandLineParser;

namespace Misfit.Console.Commands
{
    [Command("showex", "Show exception detail information of the console.", MaxArgs = 0, UsageSummary = "", UsageDescription = "Show exception detail information of the console.")]
    public class ShowExCommand : Command
    {
        public override void Execute()
        {
            int i = 0;
            if (Arguments.Count > 0)
            {
                i = Convert.ToInt32(Arguments[0]);
            }

            ShowException(i);
        }

        private void ShowException(int i)
        {
            Exception lastException = ConsoleService.LastException;
            if (lastException != null)
            {
                System.Console.Error.WriteLine(lastException);
            }
        }
    }
}
