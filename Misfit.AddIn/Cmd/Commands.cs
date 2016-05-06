using Misfit.AddIn.Pipe;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn.Cmd
{
    public class Commands
    {
        public const string Exit = "Exit";
        public const string ActivatePlugin = "ActivatePlugin";
        public const string UnLoadPlugin = "UnLoadPlugin";

        public static CommandResult Execute(string commandName)
        {
            return Execute(new CommandPacket() { CommandName = commandName });
        }

        public static CommandResult Execute(string commandName, Dictionary<string, string> parameters)
        {
            return Execute(new CommandPacket() { CommandName = commandName, Parameters = parameters });
        }

        public static CommandResult Execute(CommandPacket commandPacket)
        {
            using (NamedPipeClient namedPipeClient = new NamedPipeClient())
            {
                string commandPacketJson = JsonConvert.SerializeObject(commandPacket);

                namedPipeClient.Connect();
                namedPipeClient.WriteLine(commandPacketJson);
                string commandResultString = namedPipeClient.ReadLine();
                return JsonConvert.DeserializeObject<CommandResult>(commandResultString);
            }

        }
    }
}
