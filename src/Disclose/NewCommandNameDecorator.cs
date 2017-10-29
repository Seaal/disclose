using System;
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

        public Func<DiscloseChannel, bool> ChannelFilter => _commandHandler.ChannelFilter;

        public Func<DiscloseUser, bool> UserFilter => _commandHandler.UserFilter;

        public void Init(IDiscloseFacade disclose, IDataStore dataStore)
        {
            _commandHandler.Init(disclose, dataStore);
        }

        
        public ICommandHandler RestrictToUsers(Func<DiscloseUser, bool> user)
        {
            return _commandHandler.RestrictToUsers(user);
        }

        public Task Handle(DiscloseMessage message, string arguments)
        {
            return _commandHandler.Handle(message, arguments);
        }

        public ICommandHandler RestrictedToChannels(Func<DiscloseChannel, bool> channel)
        {
            return _commandHandler.RestrictedToChannels(channel);
        }
    }
}
