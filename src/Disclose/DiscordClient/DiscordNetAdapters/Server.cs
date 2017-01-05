using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class Server : IServer
    {
        private readonly Discord.Server _discordServer;

        public ulong Id => _discordServer.Id;
        public string Name => _discordServer.Name;

        public IEnumerable<IUser> Users => _discordServer.Users.Select(u => new User(u));

        public Server(Discord.Server server)
        {
            _discordServer = server;
        }
    }
}
