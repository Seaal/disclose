using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public class HelpCommandHandler : CommandHandler
    {
        public override string CommandName => "help";

        public override string Description => "Understand how to use commands. Use '!eh help <command name>' to find help for that specific command.";

        public override Task Handle(IMessage message, string arguments)
        {
            string response;

            if (arguments == null)
            {
                response = HandleHelpAll();
            }
            else
            {
                response = HandleHelpCommand(arguments);
            }

            return Discord.SendMessageToUser(message.User, response);
        }

        private string HandleHelpAll()
        {
            string response = "Currently available commands: \n\n";

            foreach (ICommandHandler commandHandler in Disclose.CommandHandlers)
            {
                response += $"!eh {commandHandler.CommandName} - {commandHandler.Description}";

                if (commandHandler != Disclose.CommandHandlers.Last())
                {
                    response += "\n";
                }
            }

            return response;
        }

        private string HandleHelpCommand(string command)
        {
            ICommandHandler commandHandler = Disclose.CommandHandlers.FirstOrDefault(ch => ch.CommandName.ToLowerInvariant() == command.ToLowerInvariant());

            if (commandHandler != null)
            {
                return $"!eh {commandHandler.CommandName} - {commandHandler.Description}";
            }

            return "Command not found.";
        }
    }
}
