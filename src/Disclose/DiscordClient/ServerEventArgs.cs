using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public class ServerEventArgs : EventArgs
    {
        public IServer Server { get; }

        public ServerEventArgs(IServer server)
        {
            Server = server;
        }
    }
}
