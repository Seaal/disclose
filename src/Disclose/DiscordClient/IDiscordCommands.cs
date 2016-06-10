using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public interface IDiscordCommands
    {
        ulong ClientId { get; }

        Task<IMessage> SendMessageToChannel(IChannel channel, string text);

        Task<IMessage> SendMessageToUser(IUser user, string text);
    }
}
