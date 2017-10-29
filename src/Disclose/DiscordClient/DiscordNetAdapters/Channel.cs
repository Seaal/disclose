using Discord;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    internal class Channel : IChannel
    {
        public IMessageChannel DiscordChannel { get; }

        public Channel(IMessageChannel channel)
        {
            DiscordChannel = channel;
        }

        public ulong Id => DiscordChannel.Id;
        public string Name => DiscordChannel.Name;
        public bool IsPrivateMessage => DiscordChannel is IDMChannel;
    }
}
