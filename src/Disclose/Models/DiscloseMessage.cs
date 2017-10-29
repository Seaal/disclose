using Disclose.DiscordClient;

namespace Disclose
{
    public class DiscloseMessage
    {
        public DiscloseChannel Channel { get; }
        public DiscloseUser User { get; }
        public string Content { get; }

        internal DiscloseMessage(IMessage message, DiscloseUser user)
        {
            Content = message.Content;
            Channel = new DiscloseChannel(message.Channel);
            User = user;
        }
    }
}
