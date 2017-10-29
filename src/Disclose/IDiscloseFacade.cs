using System.Collections.Generic;
using System.Threading.Tasks;

namespace Disclose
{
    public interface IDiscloseFacade
    {
        /// <summary>
        /// Every Command Handler registered with the disclose client.
        /// </summary>
        IReadOnlyCollection<ICommandHandler> CommandHandlers { get; }

        /// <summary>
        /// Every Command Handler registered with the disclose client.
        /// </summary>
        DiscloseServer Server { get; }

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
        Task<DiscloseMessage> SendMessageToChannel(DiscloseChannel channel, string text);

        /// <summary>
        /// Send a message to a user.
        /// </summary>
        /// <param name="user">The user to send the message to.</param>
        /// <param name="text">The message to send.</param>
        /// <returns></returns>
        Task<DiscloseMessage> SendMessageToUser(DiscloseUser user, string text);
    }
}
