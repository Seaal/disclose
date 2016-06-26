using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public class UserEventArgs : EventArgs
    {
        public IUser User { get; private set; }
        public IServer Server { get; private set; }

        public UserEventArgs(IUser user, IServer server)
        {
            User = user;
            Server = server;
        }
    }
}
