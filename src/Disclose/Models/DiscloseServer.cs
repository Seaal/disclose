using System.Collections.Generic;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using System.Linq;

namespace Disclose
{
    public class DiscloseServer
    {
        public ulong Id => _discordServer.Id;
        public string Name => _discordServer.Name;

        private readonly IServer _discordServer;

        internal DiscloseServer(IServer server)
        {
            _discordServer = server;
        }

        public async Task<IEnumerable<DiscloseUser>> GetUsersAsync()
        {
            return (await _discordServer.GetUsersAsync()).Select(u => new DiscloseUser(u, this));
        }

        public IEnumerable<DiscloseRole> GetRoles()
        {
            return _discordServer.GetRoles().Select(r => new DiscloseRole(r));
        }
    }
}
