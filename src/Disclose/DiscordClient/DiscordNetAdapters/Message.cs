using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class Message : IMessage
    {
        public string Text { get; set; }
        public IChannel Channel { get; }
        public IUser User { get; set; }

        public Message(Discord.Message message)
        {
            Text = message.Text;
            Channel = new Channel(message.Channel);
            User = new User(message.User);
        }
    }
}
