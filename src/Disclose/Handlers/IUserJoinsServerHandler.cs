using System.Threading.Tasks;

namespace Disclose
{
    /// <summary>
    /// A Handler for when a user first joins a Discord server.
    /// </summary>
    public interface IUserJoinsServerHandler : IHandler<IUserJoinsServerHandler>
    {
        /// <summary>
        /// Handles the event of a user joining a server for the first time.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        Task Handle(DiscloseUser user, DiscloseServer server);
    }
}
