using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public class EchoCommandHandler : ICommandHandler
    {
        public string CommandName => "echo";

        public string Description => "Repeats whatever you say back to it";
        public Task Handle(DiscloseClient client, IMessage message, string arguments)
        {
            if (arguments == null)
            {
                return Task.FromResult(0);
            }

            return Task.FromResult(0);  //e.Channel.SendMessage(arguments);
        }
    }
}
