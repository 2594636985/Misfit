using System;

namespace Misfit.Console.CommandLineParser
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CommandAttribute : Attribute, ICommandInfo
    {
        // Fields

        string commandName;
        string description;
        string usageDescription = null;
        string usageSummary = "";
        string altName;
        int minArgs = 0;
        int maxArgs = Int32.MaxValue;

        // Lifetime

        public CommandAttribute(string commandName,
                                string description)
        {
            this.commandName = commandName;
            this.description = description;
        }

        // Properties

        public string AltName
        {
            get { return altName; }
            set { altName = value; }
        }

        public string CommandName
        {
            get { return commandName; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public int MaxArgs
        {
            get { return maxArgs; }
            set { maxArgs = value; }
        }

        public int MinArgs
        {
            get { return minArgs; }
            set { minArgs = value; }
        }

        public string UsageDescription
        {
            get { return usageDescription; }
            set { usageDescription = value; }
        }

        public string UsageSummary
        {
            get { return usageSummary; }
            set { usageSummary = value; }
        }
    }
}