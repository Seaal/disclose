using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    /// <summary>
    /// Handles commands sent from a user. Must be registered with the Disclose Client.
    /// </summary>
    public interface ICommandHandler : IHandler
    {
        /// <summary>
        /// The alias for the command. The user has to type this to activate the command.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// A description of what the command does. May be used by other commands.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// This is called by the discord client when a message is sent that matches the command.
        /// </summary>
        /// <param name="message">The message received from discord</param>
        /// <param name="arguments">The arguments of the command</param>
        /// <returns></returns>
        Task Handle(IMessage message, string arguments);
    }
}
