using System;

namespace Disclose
{
    public abstract class ChannelHandler<T> : Handler<T>, IChannelHandler<T> where T : class, IChannelHandler<T>
    {
        public Func<DiscloseChannel, bool> ChannelFilter { get; private set; }

        /// <inheritdoc />
        public T RestrictedToChannels(Func<DiscloseChannel, bool> channel)
        {
            ChannelFilter = channel;

            return this as T;
        }
    }
}
