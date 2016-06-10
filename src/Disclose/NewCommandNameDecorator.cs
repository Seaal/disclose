using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace Disclose.NET
{
    internal class NewCommandNameDecorator : ICommandHandler
    {
        private readonly ICommandHandler _commandHandler;

        public NewCommandNameDecorator(ICommandHandler commandHandler, string newCommandName)
        {
            _commandHandler = commandHandler;
            Command = newCommandName;
        }

        public string Command { get; }

        public string Description => _commandHandler.Description;
        public Task Handle(DiscloseClient client, MessageEventArgs e, string arguments)
        {
            return _commandHandler.Handle(client, e, arguments);
        }
    }
}
