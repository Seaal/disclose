using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Disclose.DiscordClient
{
    public class MessageEventArgs : EventArgs
    {
        public IMessage Message { get; set; }

        public MessageEventArgs(IMessage message)
        {
            Message = message;
        }
    }
}
