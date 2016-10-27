using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class User : IUser
    {
        public Discord.User DiscordUser { get; }

        public ulong Id => DiscordUser.Id;

        public string Name => DiscordUser.Name;


        public IEnumerable<IRole> Roles => DiscordUser.Roles.Select(r => new Role(r));

        public User(Discord.User user)
        {
            DiscordUser = user;
        }
    }
}
