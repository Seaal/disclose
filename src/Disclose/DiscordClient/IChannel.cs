namespace Disclose.DiscordClient
{
    public interface IChannel
    {
        ulong Id { get; }

        string Name { get; }

        bool IsPrivateMessage { get; }
    }
}
