using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public abstract class ChannelHandler<T> : Handler, IChannelHandler<T> where T : class, IChannelHandler<T>
    {
        public Func<IChannel, bool> ChannelFilter { get; private set; }

        /// <inheritdoc />
        public T RestrictedToChannels(Func<IChannel, bool> channel)
        {
            ChannelFilter = channel;

            return this as T;
        }
    }
}
