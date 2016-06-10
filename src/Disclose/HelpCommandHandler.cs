using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace Disclose
{
    public class HelpCommandHandler : ICommandHandler
    {
        public string CommandName => "help";

        public string Description => "Understand how to use commands. Use '!eh help <command name>' to find help for that specific command.";

        public Task Handle(DiscloseClient client, MessageEventArgs e, string arguments)
        {
            string response;

            if (arguments == null)
            {
                response = HandleHelpAll(client);   
            }
            else
            {
                response = HandleHelpCommand(client, arguments);
            }

            return e.User.SendMessage(response);
        }

        private string HandleHelpAll(DiscloseClient client)
        {
            string response = "Currently available commands: \n\n";

            foreach (ICommandHandler commandHandler in client.CommandHandlers)
            {
                response += $"!eh {commandHandler.CommandName} - {commandHandler.Description}";

                if (commandHandler != client.CommandHandlers.Last())
                {
                    response += "\n";
                }
            }

            return response;
        }

        private string HandleHelpCommand(DiscloseClient client, string command)
        {
            ICommandHandler commandHandler = client.CommandHandlers.FirstOrDefault(ch => ch.CommandName.ToLowerInvariant() == command.ToLowerInvariant());

            if (commandHandler != null)
            {
                return $"!eh {commandHandler.CommandName} - {commandHandler.Description}";
            }

            return "Command not found.";
        }
    }
}
