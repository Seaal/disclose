using System.Threading.Tasks;
using Discord;

namespace Disclose
{
    public interface ICommandHandler
    {
        string CommandName { get; }

        string Description { get; }

        Task Handle(DiscloseClient client, MessageEventArgs e, string arguments);
    }
}
