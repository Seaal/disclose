using System.Collections.Generic;
using Discord;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    internal class ServerUser : User, IServerUser
    {
        public ServerUser(IGuildUser user) : base(user)
        {
            RoleIds = user.RoleIds;
        }

        public IEnumerable<ulong> RoleIds { get; }
    }
}
