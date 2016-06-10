using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface ICommandHandler
    {
        string CommandName { get; }

        string Description { get; }

        Task Handle(DiscloseClient client, IMessage message, string arguments);
    }
}
