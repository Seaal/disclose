using System;
using System.Threading.Tasks;
using Discord;

namespace Disclose.DiscordClient
{
    public interface IDiscordClient : IDiscordCommands
    {
        void ExecuteAndWait(Func<Task> action);

        Task Connect(string token);

        event EventHandler<MessageEventArgs> OnMessageReceived;
    }
}
