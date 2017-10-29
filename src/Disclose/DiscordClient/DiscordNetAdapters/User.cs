namespace Disclose.DiscordClient.DiscordNetAdapters
{
    internal class User : IUser
    {
        public Discord.IUser DiscordUser { get; }

        public ulong Id => DiscordUser.Id;

        public string Name => DiscordUser.Username;

        public User(Discord.IUser user)
        {
            DiscordUser = user;
        }
    }
}
