using System.Collections.Generic;

namespace Misfit.Console.CommandLineParser
{
    public interface ICommand
    {
        CommandLineHelper Helper { get; set; }

        List<string> Arguments { get; set; }

        void Execute();
    }
}