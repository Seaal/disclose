using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface IChannelHandler<out T> : IHandler where T : IChannelHandler<T>
    {
        Func<IChannel, bool> ChannelFilter { get; }

        /// <summary>
        /// Restrict this handler to only run on certain channels.
        /// </summary>
        /// <param name="channel">Whether to restrict the channel or not.</param>
        /// <returns>The handler.</returns>
        T RestrictedToChannels(Func<IChannel, bool> channel);
    }
}
