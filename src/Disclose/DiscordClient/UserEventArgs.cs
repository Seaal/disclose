using System;

namespace Disclose.DiscordClient
{
    public class UserEventArgs : EventArgs
    {
        public IServerUser User { get; private set; }
        public IServer Server { get; private set; }

        public UserEventArgs(IServerUser user, IServer server)
        {
            User = user;
            Server = server;
        }
    }
}
