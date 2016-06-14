using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface ICommandHandler
    {
        string CommandName { get; }

        string Description { get; }

        void Init(IDiscloseSettings disclose, IDiscordCommands discord);

        Task Handle(IMessage message, string arguments);
    }
}
