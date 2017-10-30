using Discord;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    internal class Message : IMessage
    {
        public string Content { get; }
        public IChannel Channel { get; }
        public IUser User { get; }

        public Message(Discord.IMessage message)
        {
            Content = message.Content;
            Channel = new Channel(message.Channel);

            if (message.Author is IGuildUser guildUser)
            {
                User = new ServerUser(guildUser);
            }
            else
            {
                User = new User(message.Author);
            }
        }
    }
}
