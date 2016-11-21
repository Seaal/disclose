using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    /// <summary>
    /// A Handler for when a user first joins a Discord server.
    /// </summary>
    public abstract class UserJoinsServerHandler : Handler<IUserJoinsServerHandler>, IUserJoinsServerHandler
    {
        /// <inheritdoc />
        public abstract Task Handle(IUser user, IServer server);
    }
}
