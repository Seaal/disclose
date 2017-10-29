using System;

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
