using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class Server : IServer
    {
        public ulong Id { get; }
        public string Name { get; }

        public Server(Discord.Server server)
        {
            Id = server.Id;
            Name = server.Name;
        }
    }
}
