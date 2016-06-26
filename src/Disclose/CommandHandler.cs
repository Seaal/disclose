using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    /// <summary>
    /// A base class for Command Handlers. Extend this to create your own Command Handlers.
    /// </summary>
    public abstract class CommandHandler : ICommandHandler
    {
        protected IDiscloseSettings Disclose { get; private set; }

        protected IDiscordCommands Discord { get; private set; }

        public abstract string CommandName { get; }

        public abstract string Description { get; }

        public void Init(IDiscloseSettings disclose, IDiscordCommands discord)
        {
            Disclose = disclose;
            Discord = discord;
        }

        public abstract Task Handle(IMessage message, string arguments);
    }
}
