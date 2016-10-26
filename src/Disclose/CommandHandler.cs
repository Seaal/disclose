using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public abstract class CommandHandler : ChannelHandler<ICommandHandler>, ICommandHandler
    {
        public abstract string CommandName { get; }

        public abstract string Description { get; }

        public abstract Task Handle(IMessage message, string arguments);
    }
}
