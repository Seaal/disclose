using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class User : IUser
    {
        public Discord.User DiscordUser { get; }

        public ulong Id { get; }

        public User(Discord.User user)
        {
            DiscordUser = user;
            Id = user.Id;
        }
    }
}
