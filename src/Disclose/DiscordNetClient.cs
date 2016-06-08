using System;
using System.Threading.Tasks;
using Discord;

namespace Disclose
{
    public class DiscordNetClient : IDiscordClient
    {
        private readonly DiscordClient _discordClient;

        public DiscordNetClient()
        {
            _discordClient = new DiscordClient();
        }

        event EventHandler<MessageEventArgs> IDiscordClient.OnMessageReceived
        {
            add { _discordClient.MessageReceived += value; }

            remove { _discordClient.MessageReceived -= value; }
        }

        public void ExecuteAndWait(Func<Task> action)
        {
            _discordClient.ExecuteAndWait(action);
        }

        public Task Connect(string token)
        {
            return _discordClient.Connect(token);
        }
    }
}
