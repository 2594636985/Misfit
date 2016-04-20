using System;

namespace Misfit.Console.CommandLineParser
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class OptionAttribute : Attribute
    {
        // Fields

        string description;
        string optionName;
        string shortName;
        bool showPlusMinus;

        // Lifetime

        public OptionAttribute(string optionName,
                               string description)
        {
            this.optionName = optionName;
            this.description = description;
        }

        // Properties

        public string Description
        {
            get { return description; }
        }

        public string OptionName
        {
            get { return optionName; }
        }

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        public bool ShowPlusMinus
        {
            get { return showPlusMinus; }
            set { showPlusMinus = value; }
        }
    }
}