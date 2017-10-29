using System.Collections.Generic;
using Disclose.DiscordClient;
using System.Linq;

namespace Disclose
{
    public class DiscloseUser
    {
        public ulong Id => DiscordUser.Id;
        public string Name => DiscordUser.Name;

        public IReadOnlyCollection<DiscloseRole> Roles { get; }

        internal IServerUser DiscordUser { get; }

        internal DiscloseUser(IServerUser user, DiscloseServer server)
        {
            DiscordUser = user;

            IEnumerable<DiscloseRole> roles = server.GetRoles();

            Roles = user.RoleIds.Select(ur => roles.FirstOrDefault(r => ur == r.Id)).Where(r => r != null).ToList().AsReadOnly();
        }
    }
}
