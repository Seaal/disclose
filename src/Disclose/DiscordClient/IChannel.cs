using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public interface IChannel
    {
        ulong Id { get; }

        string Name { get; }

        bool IsPrivateMessage { get; }
    }
}
