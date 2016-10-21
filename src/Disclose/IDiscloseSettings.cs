using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface IDiscloseSettings
    {
        /// <summary>
        /// Every Command Handler registered with the disclose client.
        /// </summary>
        IReadOnlyCollection<ICommandHandler> CommandHandlers { get; }
        IServer Server { get; }
    }
}
