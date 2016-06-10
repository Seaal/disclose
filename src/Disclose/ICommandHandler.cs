using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface ICommandHandler
    {
        string CommandName { get; }

        string Description { get; }

        Task Handle(IDiscloseSettings disclose, IDiscordCommands discord, IMessage message, string arguments);
    }
}
