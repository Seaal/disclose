using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public interface IServer
    {
        ulong Id { get; }
        string Name { get; }

        IEnumerable<IUser> Users { get; }
    }
}
