using Disclose.DiscordClient;

namespace Disclose
{
    public class DiscloseChannel
    {
        public ulong Id => DiscordChannel.Id;
        public string Name => DiscordChannel.Name;
        public bool IsPrivateMessage => DiscordChannel.IsPrivateMessage;

        internal IChannel DiscordChannel { get; }

        public DiscloseChannel(IChannel channel)
        {
            DiscordChannel = channel;
        }
    }
}
