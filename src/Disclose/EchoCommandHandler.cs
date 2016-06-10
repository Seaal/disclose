using System.Threading.Tasks;
using Discord;

namespace Disclose
{
    public class EchoCommandHandler : ICommandHandler
    {
        public string CommandName => "echo";

        public string Description => "Repeats whatever you say back to it";

        public Task Handle(DiscloseClient client, MessageEventArgs e, string arguments)
        {
            if (arguments == null)
            {
                return Task.FromResult(0);
            }

            return e.Channel.SendMessage(arguments);
        }
    }
}
