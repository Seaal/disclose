using System;
using System.Collections.Generic;
using System.Linq;
using Discord;

namespace Disclose
{
    public class DiscloseClient
    {
        private readonly IDiscordClient _discordClient;
        private readonly IDictionary<string, ICommandHandler> _commandHandlers;
        private DiscloseOptions _options;
        private readonly ICommandParser _parser;

        public IReadOnlyCollection<ICommandHandler> CommandHandlers => _commandHandlers.Values.ToList();

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

        public void Init(DiscloseOptions options)
        {
            _options = options;
            _parser.Init(_options);

            _discordClient.OnMessageReceived += OnMessageReceived;
        }

        public void RegisterCommandHandler(ICommandHandler commandHandler)
        {
            if (_commandHandlers.ContainsKey(commandHandler.Command))
            {
                throw new ArgumentException("A command handler with the commmand " + commandHandler.Command + " already exists!");
            }

            _commandHandlers.Add(commandHandler.Command.ToLowerInvariant(), commandHandler);
        }

        public void Connect(string token)
        {
            _discordClient.ExecuteAndWait(async () => await _discordClient.Connect(token));
        }

        private async void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.IsAuthor)
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

            await commandHandler.Handle(this, e, parsedCommand.Argument);
        }
    }
}
