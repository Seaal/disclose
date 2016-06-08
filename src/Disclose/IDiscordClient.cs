using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace Disclose.NET
{
    public interface IDiscordClient
    {
        void ExecuteAndWait(Func<Task> action);

        Task Connect(string token);

        event EventHandler<MessageEventArgs> OnMessageReceived;
    }
}
