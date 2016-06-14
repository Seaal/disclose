using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    internal class NewCommandNameDecorator : ICommandHandler
    {
        private readonly ICommandHandler _commandHandler;

        public NewCommandNameDecorator(ICommandHandler commandHandler, string newCommandNameName)
        {
            _commandHandler = commandHandler;
            CommandName = newCommandNameName;
        }

        public string CommandName { get; }

        public string Description => _commandHandler.Description;
        public void Init(IDiscloseSettings disclose, IDiscordCommands discord)
        {
            _commandHandler.Init(disclose, discord);
        }

        public Task Handle(IMessage message, string arguments)
        {
            return _commandHandler.Handle(message, arguments);
        }
    }
}
