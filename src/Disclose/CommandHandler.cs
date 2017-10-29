using System.Threading.Tasks;

namespace Disclose
{
    public abstract class CommandHandler : ChannelHandler<ICommandHandler>, ICommandHandler
    {
        public abstract string CommandName { get; }

        public abstract string Description { get; }

        public abstract Task Handle(DiscloseMessage message, string arguments);
    }
}
