using System;

namespace Disclose
{
    public interface IChannelHandler<T> : IHandler<T> where T : IChannelHandler<T>
    {
        Func<DiscloseChannel, bool> ChannelFilter { get; }

        /// <summary>
        /// Restrict this handler to only run on certain channels.
        /// </summary>
        /// <param name="channel">Whether to restrict the channel or not.</param>
        /// <returns>The handler.</returns>
        T RestrictedToChannels(Func<DiscloseChannel, bool> channel);
    }
}
