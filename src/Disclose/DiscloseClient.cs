using System;
using System.Collections.Generic;
using System.Linq;
using Disclose.DiscordClient;
using Disclose.DiscordClient.DiscordNetAdapters;
using MessageEventArgs = Disclose.DiscordClient.MessageEventArgs;
using ServerEventArgs = Disclose.DiscordClient.ServerEventArgs;
using UserEventArgs = Disclose.DiscordClient.UserEventArgs;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Disclose.Tests")]

namespace Disclose
{
    public class DiscloseClient : IDiscloseFacade
    {
        private readonly IDiscordClient _discordClient;
        private readonly IDictionary<string, ICommandHandler> _commandHandlers;
        private readonly ICollection<IUserJoinsServerHandler> _userJoinsServerHandlers;
        private DiscloseOptions _options;
        private readonly ICommandParser _parser;
        private DiscloseServer _server;
        private DataStoreLockDecorator _decoratedDataStore;

        IReadOnlyCollection<ICommandHandler> IDiscloseFacade.CommandHandlers => _commandHandlers.Values.ToList();
        DiscloseServer IDiscloseFacade.Server => _server;
        ulong IDiscloseFacade.ClientId => _discordClient.ClientId;

        async Task<DiscloseMessage> IDiscloseFacade.SendMessageToChannel(DiscloseChannel channel, string text)
        {
            IMessage message = await _discordClient.SendMessageToChannel(channel.DiscordChannel, text);

            IEnumerable<DiscloseUser> users = await _server.GetUsersAsync();

            return new DiscloseMessage(message, users.FirstOrDefault(u => u.Id == _discordClient.ClientId));
        }

        async Task<DiscloseMessage> IDiscloseFacade.SendMessageToUser(DiscloseUser user, string text)
        {
            IMessage message = await _discordClient.SendMessageToUser(user.DiscordUser, text);

            IEnumerable<DiscloseUser> users = await _server.GetUsersAsync();

            return new DiscloseMessage(message, users.FirstOrDefault(u => u.Id == _discordClient.ClientId));
        }

        /// <summary>
        /// Provides a way of storing data to handlers.
        /// </summary>
        public IDataStore DataStore {
            get
            {
                return _decoratedDataStore?.DataStore;
            }
            set
            {
                _decoratedDataStore = new DataStoreLockDecorator(value);
            }
        }

        /// <summary>
        /// Creates an instance of the DiscloseClient with default implementations and calls Init to set the disclose options.
        /// </summary>
        /// <param name="setOptions">Set your Disclose options here.</param>
        /// <returns></returns>
        public static DiscloseClient Bootstrap(Action<DiscloseOptions> setOptions)
        {
            DiscloseOptions options = new DiscloseOptions();

            setOptions(options);

            DiscloseClient client = new DiscloseClient(new DiscordNetClient(), new CommandParser());

            client.Init(options);

            return client;
        }

        public DiscloseClient(IDiscordClient discordClient, ICommandParser parser)
        {
            _discordClient = discordClient;
            _parser = parser;

            _commandHandlers = new Dictionary<string, ICommandHandler>();
            _userJoinsServerHandlers = new List<IUserJoinsServerHandler>();
        }

        public DiscloseClient(IDiscordClient discordClient, ICommandParser parser, IDataStore dataStore) : this(discordClient, parser)
        {
            DataStore = dataStore;
        }

        /// <summary>
        /// Sets the Disclose options. Must be called before Connect is called.
        /// </summary>
        /// <param name="options"></param>
        public void Init(DiscloseOptions options)
        {
            _options = options;
            _parser.Init(_options);

            _discordClient.OnMessageReceived += OnMessageReceived;
            _discordClient.OnUserJoinedServer += OnUserJoinedServer;
            _discordClient.OnServerAvailable += OnServerAvailable;
        }

        /// <summary>
        /// Registers a command handler to handle commands sent to the bot.
        /// </summary>
        /// <param name="commandHandler"></param>
        public void Register(ICommandHandler commandHandler)
        {
            if (commandHandler == null)
            {
                throw new ArgumentNullException(nameof(commandHandler));
            }

            if (String.IsNullOrWhiteSpace(commandHandler.CommandName))
            {
                throw new ArgumentException("CommandName must contain a non whitespace character");
            }

            if (_commandHandlers.ContainsKey(commandHandler.CommandName))
            {
                throw new ArgumentException("A command handler with the commmand " + commandHandler.CommandName + " already exists!");
            }

            commandHandler.Init(this, _decoratedDataStore);

            _commandHandlers.Add(commandHandler.CommandName.ToLowerInvariant(), commandHandler);
        }

        /// <summary>
        /// Registers a handler to handle when a new user joins the server for the first time.
        /// </summary>
        /// <param name="userJoinsServerHandler"></param>
        public void Register(IUserJoinsServerHandler userJoinsServerHandler)
        {
            if (userJoinsServerHandler == null)
            {
                throw new ArgumentNullException(nameof(userJoinsServerHandler));
            }

            userJoinsServerHandler.Init(this, _decoratedDataStore);

            _userJoinsServerHandlers.Add(userJoinsServerHandler);
        }

        /// <summary>
        /// Runs an installer, adding the installer's handlers to the disclose.
        /// </summary>
        /// <param name="installer"></param>
        public void Install(IInstaller installer)
        {
            if (installer == null)
            {
                throw new ArgumentNullException(nameof(installer));
            }

            installer.Install(this);
        }

        /// <summary>
        /// Connect to Discord and wait for requests.
        /// </summary>
        /// <param name="token"></param>
        public Task Connect(string token)
        {
            return _discordClient.Connect(token);
        }

        private async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            IMessage message = e.Message;

            if (message.User.Id == _discordClient.ClientId)
            {
                return;
            }

            ParsedCommand parsedCommand = _parser.ParseCommand(message);

            if (!parsedCommand.Success)
            {
                return;
            }

            bool commandExists = _commandHandlers.TryGetValue(parsedCommand.Command, out ICommandHandler commandHandler);

            if (!commandExists)
            {
                return;
            }

            if (commandHandler.ChannelFilter != null && !commandHandler.ChannelFilter(new DiscloseChannel(message.Channel)))
            {
                return;
            }

            DiscloseUser discloseUser;

            if (message.Channel.IsPrivateMessage)
            {
                //User objects in Direct Messages don't have roles because there is no server context. So find the user on the server and use that user to have the user's roles available
                IEnumerable<DiscloseUser> serverUsers = await _server.GetUsersAsync();

                discloseUser = serverUsers.FirstOrDefault(su => su.Id == message.User.Id);

                if (discloseUser == null)
                {
                    return;
                }
            }
            else
            {
                discloseUser = new DiscloseUser((IServerUser)message.User, _server);
            }

            DiscloseMessage discloseMessage = new DiscloseMessage(message, discloseUser);

            if (commandHandler.UserFilter != null && !commandHandler.UserFilter(discloseMessage.User))
            {
                return;
            }

            await commandHandler.Handle(discloseMessage, parsedCommand.Argument);
        }

        private async void OnUserJoinedServer(object sender, UserEventArgs e)
        {
            foreach (IUserJoinsServerHandler handler in _userJoinsServerHandlers)
            {
                try
                {
                    await handler.Handle(new DiscloseUser(e.User, _server), _server);
                }
                catch (Exception)
                {
                    //Suppress errors here, if one handler fails, we still want the others to run
                }
            }
        }

        private void OnServerAvailable(object sender, ServerEventArgs e)
        {
            DiscloseServer server = new DiscloseServer(e.Server);

            if (_server?.Id == e.Server.Id)
            {
                return;
            }

            if (_server == null && (_options.ServerFilter == null || _options.ServerFilter(server)))
            {
                _server = server;
            }
            else if (_server != null && (_options.ServerFilter == null || _options.ServerFilter(server)))
            {
                throw new InvalidOperationException("More than 1 server matched your filter, or no filter was supplied, make your filter more specific.");
            }
        }
    }
}
