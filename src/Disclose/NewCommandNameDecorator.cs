﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace Disclose.NET
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
        public Task Handle(DiscloseClient client, MessageEventArgs e, string arguments)
        {
            return _commandHandler.Handle(client, e, arguments);
        }
    }
}
