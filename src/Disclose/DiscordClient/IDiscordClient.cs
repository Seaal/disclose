using System;
using System.Threading.Tasks;
using Discord;

namespace Disclose.DiscordClient
{
    public interface IDiscordClient
    {
        ulong ClientId { get; }

        void ExecuteAndWait(Func<Task> action);

        Task Connect(string token);

        event EventHandler<MessageEventArgs> OnMessageReceived;
    }
}
