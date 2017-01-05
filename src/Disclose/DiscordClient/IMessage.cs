using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public interface IMessage
    {
        string Text { get; }

        IChannel Channel { get; }

        IUser User { get; }
    }
}
