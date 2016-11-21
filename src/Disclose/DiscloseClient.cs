using System;
using System.Collections.Generic;
using System.Linq;
using Disclose.DiscordClient;
using Disclose.DiscordClient.DiscordNetAdapters;
using MessageEventArgs = Disclose.DiscordClient.MessageEventArgs;
using ServerEventArgs = Disclose.DiscordClient.ServerEventArgs;
using UserEventArgs = Disclose.DiscordClient.UserEventArgs;

namespace Disclose
{
    public class DiscloseClient : IDiscloseSettings
    {
        private readonly IDiscordClient _discordClient;
        private readonly IDictionary<string, ICommandHandler> _commandHandlers;
        private readonly ICollection<IUserJoinsServerHandler> _userJoinsServerHandlers;
        private DiscloseOptions _options;
        private readonly ICommandParser _parser;
        private IServer _server;

        IReadOnlyCollection<ICommandHandler> IDiscloseSettings.CommandHandlers => _commandHandlers.Values.ToList();
        IServer IDiscloseSettings.Server => _server;

        /// <summary>
        /// Provides a way of storing data to handlers.
        /// </summary>
        public IDataStore DataStore { get; set; }

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

            commandHandler.Init(this, _discordClient, DataStore);

            _commandHandlers.Add(commandHandler.CommandName.ToLowerInvariant(), commandHandler);
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
        /// Registers a handler to handle when a new user joins the server for the first time.
        /// </summary>
        /// <param name="userJoinsServerHandler"></param>
        public void Register(IUserJoinsServerHandler userJoinsServerHandler)
        {
            if (userJoinsServerHandler == null)
            {
                throw new ArgumentNullException(nameof(userJoinsServerHandler));
            }

            userJoinsServerHandler.Init(this, _discordClient, DataStore);

            _userJoinsServerHandlers.Add(userJoinsServerHandler);
        }

        /// <summary>
        /// Connect to Discord and wait for requests. This will suspend console applications.
        /// </summary>
        /// <param name="token"></param>
        public void Connect(string token)
        {
            _discordClient.ExecuteAndWait(async () => await _discordClient.Connect(token));
        }

        private async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.User.Id == _discordClient.ClientId)
            {
                return;
            }

            ParsedCommand parsedCommand = _parser.ParseCommand(e.Message);

            if (!parsedCommand.Success)
            {
                return;
            }

            ICommandHandler commandHandler;

            bool commandExists = _commandHandlers.TryGetValue(parsedCommand.Command, out commandHandler);

            if (!commandExists)
            {
                return;
            }

            if (commandHandler.ChannelFilter != null && !commandHandler.ChannelFilter(e.Message.Channel))
            {
                return;
            }

            if (commandHandler.UserFilter != null && !commandHandler.UserFilter(e.Message.User))
            {
                return;
            }

            await commandHandler.Handle(e.Message, parsedCommand.Argument);
        }

        private async void OnUserJoinedServer(object sender, UserEventArgs e)
        {
            foreach (IUserJoinsServerHandler handler in _userJoinsServerHandlers)
            {
                try
                {
                    await handler.Handle(e.User, e.Server);
                }
                catch (Exception)
                {
                    //Suppress errors here, if one handler fails, we still want the others to run
                }
            }
        }

        private void OnServerAvailable(object sender, ServerEventArgs e)
        {
            if (_server?.Id == e.Server.Id)
            {
                return;
            }

            if (_server == null && (_options.ServerFilter == null || _options.ServerFilter(e.Server)))
            {
                _server = e.Server;
            }
            else if (_server != null && (_options.ServerFilter == null || _options.ServerFilter(e.Server)))
            {
                throw new InvalidOperationException("More than 1 server matched your filter, or no filter was supplied, make your filter more specific.");
            }
        }
    }
}
