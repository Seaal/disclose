using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class Role : IRole
    {
        public ulong Id { get; }
        public string Name { get; }

        public Role(Discord.Role discordRole)
        {
            Id = discordRole.Id;
            Name = discordRole.Name;
        }
    }
}
