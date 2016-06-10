using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public class EchoCommandHandler : ICommandHandler
    {
        public string CommandName => "echo";

        public string Description => "Repeats whatever you say back to it";
        public Task Handle(IDiscloseSettings disclose, IDiscordCommands discord, IMessage message, string arguments)
        {
            if (arguments == null)
            {
                return Task.FromResult(0);
            }

            return discord.SendMessageToChannel(message.Channel, arguments);
        }
    }
}
