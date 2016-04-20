using System;
using System.Collections.Generic;
using System.Reflection;
using Misfit.Console.CommandLineParser;
using Misfit.Console.Utility;

namespace Misfit.Console.Views
{
    public class HelpConsoleView
    {
        // Fields

        CommandLineHelper commandLineHelper;
        string helpUrl;
        string productName;

        // Lifetime

        public HelpConsoleView(CommandLineHelper commandLineHelper, string productName)
            : this(commandLineHelper, productName, null) { }

        public HelpConsoleView(CommandLineHelper commandLineHelper, string productName,
                               string helpUrl)
        {
            this.commandLineHelper = commandLineHelper;
            this.productName = productName;
            this.helpUrl = helpUrl;
        }

        // Methods

        static string GetAltText(string altNameText)
        {
            if (altNameText == null)
                return "";
            return string.Format(" ({0})", altNameText);
        }

        static string OptionToDisplayText(KeyValuePair<OptionAttribute, PropertyInfo> pair)
        {
            string optionFlag = "";
            string argText = "";

            if (pair.Value.PropertyType != typeof(bool))
                argText = " <arg>";
            else if (pair.Key.ShowPlusMinus)
                optionFlag = "(-)";

            string result = string.Format("/{0}{1}{2}", pair.Key.OptionName, optionFlag, argText).PadRight(24).Substring(0, 24);

            if (pair.Key.ShortName != null)
            {
                string altName = string.Format("[/{0}]", pair.Key.ShortName);
                result = result.Substring(0, result.Length - altName.Length) + altName;
            }

            return result;
        }

        public void ViewHelp()
        {
            System.Console.WriteLine("usage: <command> [options] [args]");
            System.Console.WriteLine("{0}, description {1}", productName, ReflectionUtil.GetAssemblyVersion(Assembly.GetEntryAssembly()));
            System.Console.WriteLine("Value 'help <command>' for help on a specific command.");
            System.Console.WriteLine();
            System.Console.WriteLine("Available commands:");

            List<string> commandNames = new List<string>();

            foreach (ICommandInfo attrib in commandLineHelper.GetCommands().Keys)
                commandNames.Add(attrib.CommandName + GetAltText(attrib.AltName));

            commandNames.Sort();

            foreach (string commandName in commandNames)
                System.Console.WriteLine("  {0}", commandName);

            if (helpUrl != null)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("For more information, visit {0}", helpUrl);
            }
        }

        public void ViewHelpForCommand(string commandName)
        {
            KeyValuePair<ICommandInfo, ICommand> commandPair = commandLineHelper.BuildUp(commandName);

            if (commandPair.Key == null)
                throw new CommandLineException("Unknown command: '{0}'", commandName);

            ICommandInfo attribute = commandPair.Key;
            ICommand command = commandPair.Value;

            System.Console.WriteLine("{0}{1}: {2}", attribute.CommandName, GetAltText(attribute.AltName), attribute.Description);
            System.Console.WriteLine("usage: {0} {1}", attribute.CommandName, attribute.UsageSummary);

            if (attribute.UsageDescription != null)
            {
                System.Console.WriteLine();

                foreach (string line in attribute.UsageDescription.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                    System.Console.WriteLine("  {0}", line);
            }

            Dictionary<OptionAttribute, PropertyInfo> options = commandLineHelper.GetCommandOptions(command);

            if (options.Count > 0)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Valid options:");

                foreach (KeyValuePair<OptionAttribute, PropertyInfo> pair in options)
                {
                    System.Console.WriteLine("  {0} : {1}", OptionToDisplayText(pair), pair.Key.Description);
                }
            }
        }
    }
}