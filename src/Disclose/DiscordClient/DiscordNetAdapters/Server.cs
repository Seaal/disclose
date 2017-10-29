using Discord;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    internal class Server : IServer
    {
        private readonly IGuild _guild;

        public ulong Id => _guild.Id;
        public string Name => _guild.Name;

        public Server(IGuild server)
        {
            _guild = server;
        }

        public async Task<IEnumerable<IServerUser>> GetUsersAsync()
        {
            return (await _guild.GetUsersAsync()).Select(u => new ServerUser(u));
        }

        public IEnumerable<IRole> GetRoles()
        {
            return _guild.Roles.Select(r => new Role(r));
        }
    }
}
