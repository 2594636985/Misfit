using System;

namespace Misfit.Console.CommandLineParser
{
    public class CommandLineException : Exception
    {
        public CommandLineException(string message)
            : base(message) {}

        public CommandLineException(string format,
                                    params object[] args)
            : base(string.Format(format, args)) {}
    }
}