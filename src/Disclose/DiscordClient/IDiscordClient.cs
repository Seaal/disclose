using System;
using System.Threading.Tasks;

namespace Disclose.DiscordClient
{
    public interface IDiscordClient : IDiscordCommands
    {
        void ExecuteAndWait(Func<Task> action);

        Task Connect(string token);

        event EventHandler<MessageEventArgs> OnMessageReceived;
        event EventHandler<UserEventArgs> OnUserJoinedServer;
        event EventHandler<ServerEventArgs> OnServerAvailable;

    }
}
