namespace Disclose.DiscordClient
{
    public interface IMessage
    {
        string Content { get; }

        IChannel Channel { get; }

        IUser User { get; }
    }
}
