using Disclose.DiscordClient;

namespace Disclose
{
    public class DiscloseRole
    {
        public ulong Id { get; }
        public string Name { get; }

        internal DiscloseRole(IRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}
