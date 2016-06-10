using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class Channel : IChannel
    {
        public Discord.Channel DiscordChannel { get; }

        public Channel(Discord.Channel channel)
        {
            DiscordChannel = channel;
        }
    }
}
