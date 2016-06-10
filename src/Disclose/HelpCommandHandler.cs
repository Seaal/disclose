using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public class HelpCommandHandler : ICommandHandler
    {
        public string CommandName => "help";

        public string Description => "Understand how to use commands. Use '!eh help <command name>' to find help for that specific command.";
        public Task Handle(IDiscloseSettings disclose, IDiscordCommands discord, IMessage message, string arguments)
        {
            string response;

            if (arguments == null)
            {
                response = HandleHelpAll(disclose);
            }
            else
            {
                response = HandleHelpCommand(disclose, arguments);
            }

            return discord.SendMessageToUser(message.User, response);
        }

        private string HandleHelpAll(IDiscloseSettings disclose)
        {
            string response = "Currently available commands: \n\n";

            foreach (ICommandHandler commandHandler in disclose.CommandHandlers)
            {
                response += $"!eh {commandHandler.CommandName} - {commandHandler.Description}";

                if (commandHandler != disclose.CommandHandlers.Last())
                {
                    response += "\n";
                }
            }

            return response;
        }

        private string HandleHelpCommand(IDiscloseSettings disclose, string command)
        {
            ICommandHandler commandHandler = disclose.CommandHandlers.FirstOrDefault(ch => ch.CommandName.ToLowerInvariant() == command.ToLowerInvariant());

            if (commandHandler != null)
            {
                return $"!eh {commandHandler.CommandName} - {commandHandler.Description}";
            }

            return "Command not found.";
        }
    }
}
