using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public class EchoCommandHandler : CommandHandler
    {
        public override string CommandName => "echo";

        public override string Description => "Repeats whatever you say back to it";

        public override Task Handle(IMessage message, string arguments)
        {
            if (arguments == null)
            {
                return Task.FromResult(0);
            }

            return Discord.SendMessageToChannel(message.Channel, arguments);
        }
    }
}
