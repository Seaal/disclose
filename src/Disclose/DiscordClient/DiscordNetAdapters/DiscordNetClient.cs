using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Disclose.DiscordClient.DiscordNetAdapters
{
    public class DiscordNetClient : IDiscordClient
    {
        private readonly DiscordSocketClient _discordClient;

        public DiscordNetClient()
        {
            _discordClient = new DiscordSocketClient();
            _discordClient.MessageReceived += OnDiscordMessageReceived;
            _discordClient.UserJoined += OnDiscordUserJoined;
            _discordClient.GuildAvailable += OnDiscordServerAvailable;
        }

        public event EventHandler<MessageEventArgs> OnMessageReceived;
        public event EventHandler<UserEventArgs> OnUserJoinedServer;
        public event EventHandler<ServerEventArgs> OnServerAvailable;

        public async Task<IMessage> SendMessageToChannel(IChannel channel, string text)
        {
            Channel realChannel = (Channel) channel;

            return new Message(await realChannel.DiscordChannel.SendMessageAsync(text));
        }

        public async Task<IMessage> SendMessageToUser(IUser user, string text)
        {
            User realUser = (User) user;

            return new Message(await realUser.DiscordUser.SendMessageAsync(text));
        }

        private Task OnDiscordMessageReceived(SocketMessage message)
        {
            OnMessageReceived?.Invoke(this, new MessageEventArgs(new Message(message)));

            return Task.FromResult(0);
        }

        private Task OnDiscordUserJoined(SocketGuildUser user)
        {
            OnUserJoinedServer?.Invoke(this, new UserEventArgs(new ServerUser(user), new Server(user.Guild)));

            return Task.FromResult(0);
        }

        private Task OnDiscordServerAvailable(SocketGuild guild)
        {
            OnServerAvailable?.Invoke(this, new ServerEventArgs(new Server(guild)));

            return Task.FromResult(0);
        }

        public ulong ClientId => _discordClient.CurrentUser.Id;

        public async Task Connect(string token)
        {
            await _discordClient.LoginAsync(TokenType.Bot, token);

            await _discordClient.StartAsync();
        }
    }
}
