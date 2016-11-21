using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

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
        Task Handle(IUser user, IServer server);
    }
}
