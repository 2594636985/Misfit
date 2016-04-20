using System;
using System.Collections.Generic;
using System.Reflection;

namespace Misfit.Console.CommandLineParser
{
    public class CommandLineHelper
    {
        private Dictionary<ICommandInfo, ICommand> commands;

        public CommandLineHelper()
        {
            commands = new Dictionary<ICommandInfo, ICommand>();
        }

        public void RegisterCommand(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }
            foreach (Type type in assembly.GetExportedTypes())
            {
                foreach (CommandAttribute attribute in type.GetCustomAttributes(typeof(CommandAttribute), true))
                {
                    ICommand command = Activator.CreateInstance(type) as ICommand;
                    if (command != null)
                        RegisterCommand(command);
                }
            }
        }

        public ICommandInfo FindByCommand(ICommand command)
        {
            foreach (KeyValuePair<ICommandInfo, ICommand> pair in GetCommands())
                if (pair.Value.GetType() == command.GetType())
                    return pair.Key;

            return null;
        }

        public KeyValuePair<ICommandInfo, ICommand> BuildUp(string cmdName)
        {
            foreach (KeyValuePair<ICommandInfo, ICommand> kvp in GetCommands())
                if ((string.Compare(kvp.Key.CommandName, cmdName, true) == 0) || (string.Compare(kvp.Key.AltName, cmdName, true) == 0))
                {
                    ICommand command = (ICommand)Activator.CreateInstance(kvp.Value.GetType());
                    command.Helper = this;
                    return new KeyValuePair<ICommandInfo, ICommand>(kvp.Key, command);
                }

            return new KeyValuePair<ICommandInfo, ICommand>();
        }

        [Obsolete]
        public KeyValuePair<ICommandInfo, ICommand> FindByCommandName(string cmdName)
        {
            foreach (KeyValuePair<ICommandInfo, ICommand> kvp in GetCommands())
                if ((string.Compare(kvp.Key.CommandName, cmdName, true) == 0) || (string.Compare(kvp.Key.AltName, cmdName, true) == 0))
                    return new KeyValuePair<ICommandInfo, ICommand>(kvp.Key, kvp.Value);

            return new KeyValuePair<ICommandInfo, ICommand>();
        }

        public Dictionary<OptionAttribute, PropertyInfo> GetCommandOptions(ICommand cmd)
        {
            Dictionary<OptionAttribute, PropertyInfo> result = new Dictionary<OptionAttribute, PropertyInfo>();

            foreach (PropertyInfo propInfo in cmd.GetType().GetProperties())
            {
                foreach (OptionAttribute attr in propInfo.GetCustomAttributes(typeof(OptionAttribute), true))
                {
                    if (!propInfo.CanWrite)
                        throw new InvalidOperationException("[Option] on " + cmd.GetType().FullName + "." + propInfo.Name + " is invalid without a setter");

                    result.Add(attr, propInfo);
                }
            }

            return result;
        }

        public Dictionary<ICommandInfo, ICommand> GetCommands()
        {
            return commands;
        }

        public void RegisterCommand<TCommand>()
            where TCommand : ICommand
        {
            ICommand command = Activator.CreateInstance<TCommand>();
            RegisterCommand(command);
        }

        public void RegisterCommand(ICommand command)
        {
            foreach (CommandAttribute attrib in command.GetType().GetCustomAttributes(typeof(CommandAttribute), true))
            {
                commands.Add(attrib, command);
            }
        }
    }
}