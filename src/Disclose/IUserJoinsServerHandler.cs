using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
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
