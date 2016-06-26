using System;
using System.Collections.Generic;
using System.Linq;
using Disclose.DiscordClient;
using Disclose.DiscordClient.DiscordNetAdapters;

namespace Disclose
{
    public class DiscloseClient : IDiscloseSettings
    {
        private readonly IDiscordClient _discordClient;
        private readonly IDictionary<string, ICommandHandler> _commandHandlers;
        private DiscloseOptions _options;
        private readonly ICommandParser _parser;

        IReadOnlyCollection<ICommandHandler> IDiscloseSettings.CommandHandlers => _commandHandlers.Values.ToList();

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
        }

        /// <summary>
        /// Registers a command handler to handle commands sent to the bot.
        /// </summary>
        /// <param name="commandHandler"></param>
        public void RegisterCommandHandler(ICommandHandler commandHandler)
        {
            if (String.IsNullOrWhiteSpace(commandHandler.CommandName))
            {
                throw new ArgumentException("CommandName must contain a non whitespace character");
            }

            if (_commandHandlers.ContainsKey(commandHandler.CommandName))
            {
                throw new ArgumentException("A command handler with the commmand " + commandHandler.CommandName + " already exists!");
            }

            commandHandler.Init(this, _discordClient);

            _commandHandlers.Add(commandHandler.CommandName.ToLowerInvariant(), commandHandler);
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

            ParsedCommand parsedCommand = _parser.ParseCommand(e.Message.Text);

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

            await commandHandler.Handle(e.Message, parsedCommand.Argument);
        }
    }
}
