using System;
using System.Threading.Tasks;
using Discord;

namespace Disclose
{
    public interface IDiscordClient
    {
        void ExecuteAndWait(Func<Task> action);

        Task Connect(string token);

        event EventHandler<MessageEventArgs> OnMessageReceived;
    }
}
