using System;
using System.Collections.Generic;
using System.Reflection;

namespace Misfit.Console.CommandLineParser
{
    public class CommandLineParser
    {
        // Fields

        CommandLineHelper commandLineHelper;

        // Lifetime

        public CommandLineParser(CommandLineHelper commandLineHelper)
        {
            this.commandLineHelper = commandLineHelper;
        }

        // Methods

        ICommand ExtractOptions(ICommand cmd,
                                string commandLine)
        {
            List<string> arguments = new List<string>();
            Dictionary<OptionAttribute, PropertyInfo> properties = commandLineHelper.GetCommandOptions(cmd);

            while (true)
            {
                string option = GetNextCommandLineItem(ref commandLine);
                //string option = commandLine;

                if (option == "")
                    break;

                if (option.Length < 2 || !option.StartsWith("/"))
                {
                    arguments.Add(option);
                    continue;
                }

                PropertyInfo propInfo = null;
                string optionText = option.Substring(1);
                string value = null;

                if (optionText.EndsWith("-"))
                {
                    optionText = optionText.TrimEnd('-');
                    value = "false";
                }

                foreach (KeyValuePair<OptionAttribute, PropertyInfo> pair in properties)
                    if (string.Compare(pair.Key.OptionName, optionText, true) == 0 || string.Compare(pair.Key.ShortName, optionText, true) == 0)
                    {
                        propInfo = pair.Value;
                        break;
                    }

                if (propInfo == null)
                    throw new CommandLineException("Unknown option: {0}", option);

                if (propInfo.PropertyType == typeof(bool))
                    value = value ?? "true";
                else
                    value = GetNextCommandLineItem(ref commandLine);

                if (value == "")
                    throw new CommandLineException("Missing option owner for: {0}", option);

                try
                {
                    propInfo.SetValue(cmd, Convert.ChangeType(value, propInfo.PropertyType), null);
                }
                catch (Exception)
                {
                    throw new CommandLineException("Invalid option owner: {0} {1}", option, value);
                }
            }

            cmd.Arguments = arguments;
            return cmd;
        }

        public string GetCommandName(ICommand command)
        {
            return commandLineHelper.FindByCommand(command).CommandName;
        }

        static string GetNextCommandLineItem(ref string commandLine)
        {
            bool inQuotes = false;
            int idx = 0;

            commandLine = commandLine.Trim();

            while (idx < commandLine.Length)
            {
                if (commandLine[idx] == ' ' && !inQuotes)
                    break;
                if (commandLine[idx] == '"')
                    inQuotes = !inQuotes;

                ++idx;
            }

            string result = commandLine.Substring(0, idx);

            if (idx < commandLine.Length)
                commandLine = commandLine.Substring(idx + 1);
            else
                commandLine = "";

            return result.Trim('"');
        }

        public ICommand ParseCommandLine(string commandLine)
        {
            string cmdName = GetNextCommandLineItem(ref commandLine);

            if (cmdName == "")
                return null;

            KeyValuePair<ICommandInfo, ICommand> cmdPair = commandLineHelper.BuildUp(cmdName);
            //KeyValuePair<ICommandInfo, ICommand> cmdPair = commandLineHelper.FindByCommandName(cmdName);

            if (cmdPair.Key == null)
                throw new CommandLineException("Unknown command: '{0}'", cmdName);

            return ExtractOptions(cmdPair.Value, commandLine);
        }

        public bool ArgCountTooLow(ICommand command)
        {
            return command.Arguments.Count < commandLineHelper.FindByCommand(command).MinArgs;
        }

        public bool ArgCountTooHigh(ICommand command)
        {
            return command.Arguments.Count > commandLineHelper.FindByCommand(command).MaxArgs;
        }
    }
}