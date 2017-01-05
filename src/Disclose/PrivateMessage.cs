using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    internal class PrivateMessage : IMessage
    {
        public string Text { get; }
        public IChannel Channel { get; }
        public IUser User { get; }

        public PrivateMessage(IMessage message, IUser user)
        {
            Text = message.Text;
            Channel = message.Channel;
            User = user;
        }
    }
}
