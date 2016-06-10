using System;
using System.Threading.Tasks;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class DiscordNetClient : IDiscordClient
    {
        private readonly Discord.DiscordClient _discordClient;

        public DiscordNetClient()
        {
            _discordClient = new Discord.DiscordClient();
            _discordClient.MessageReceived += OnDiscordMessageReceived;
        }

        public event EventHandler<MessageEventArgs> OnMessageReceived;

        public void OnDiscordMessageReceived(object sender, Discord.MessageEventArgs e)
        {
            OnMessageReceived?.Invoke(this, new MessageEventArgs(new Message(e.Message)));
        }

        public ulong ClientId => _discordClient.CurrentUser.Id;

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
