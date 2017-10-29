using System.Threading.Tasks;

namespace Disclose
{
    /// <summary>
    /// A Handler for when a user first joins a Discord server.
    /// </summary>
    public abstract class UserJoinsServerHandler : Handler<IUserJoinsServerHandler>, IUserJoinsServerHandler
    {
        /// <inheritdoc />
        public abstract Task Handle(DiscloseUser user, DiscloseServer server);
    }
}
