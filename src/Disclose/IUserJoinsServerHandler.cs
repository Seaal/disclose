using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface IUserJoinsServerHandler : IHandler
    {
        Task Handle(IUser user, IServer server);
    }
}
