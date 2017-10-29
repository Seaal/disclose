using System;

namespace Disclose.DiscordClient
{
    public class MessageEventArgs : EventArgs
    {
        public IMessage Message { get; set; }

        public MessageEventArgs(IMessage message)
        {
            Message = message;
        }
    }
}
