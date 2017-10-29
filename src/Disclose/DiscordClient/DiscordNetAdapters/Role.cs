namespace Disclose.DiscordClient.DiscordNetAdapters
{
    internal class Role : IRole
    {
        public ulong Id { get; }
        public string Name { get; }

        public Role(Discord.IRole discordRole)
        {
            Id = discordRole.Id;
            Name = discordRole.Name;
        }
    }
}
