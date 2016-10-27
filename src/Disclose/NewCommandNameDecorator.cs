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

        public Func<IChannel, bool> ChannelFilter => _commandHandler.ChannelFilter;

        public Func<IUser, bool> UserFilter => _commandHandler.UserFilter;

        public void Init(IDiscloseSettings disclose, IDiscordCommands discord, IDataStore dataStore)
        {
            _commandHandler.Init(disclose, discord, dataStore);
        }

        
        public ICommandHandler RestrictToUsers(Func<IUser, bool> user)
        {
            return _commandHandler.RestrictToUsers(user);
        }

        public Task Handle(IMessage message, string arguments)
        {
            return _commandHandler.Handle(message, arguments);
        }

        public ICommandHandler RestrictedToChannels(Func<IChannel, bool> channel)
        {
            return _commandHandler.RestrictedToChannels(channel);
        }
    }
}
