using System.Threading.Tasks;
using Discord;

namespace Disclose
{
    public interface ICommandHandler
    {
        string Command { get; }

        string Description { get; }

        Task Handle(DiscloseClient client, MessageEventArgs e, string arguments);
    }
}
