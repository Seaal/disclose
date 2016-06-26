using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public interface IDiscordCommands
    {
        /// <summary>
        /// The bot's client id.
        /// </summary>
        ulong ClientId { get; }

        /// <summary>
        /// Send a message to a channel.
        /// </summary>
        /// <param name="channel">The channel to send the message to.</param>
        /// <param name="text">The message to send.</param>
        /// <returns></returns>
        Task<IMessage> SendMessageToChannel(IChannel channel, string text);

        /// <summary>
        /// Send a message to a user.
        /// </summary>
        /// <param name="user">The user to send the message to.</param>
        /// <param name="text">The message to send.</param>
        /// <returns></returns>
        Task<IMessage> SendMessageToUser(IUser user, string text);
    }
}
