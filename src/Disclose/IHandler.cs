using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface IHandler
    {
        /// <summary>
        /// Called when the command handler is registered, so you have access to disclose's services.
        /// </summary>
        /// <param name="disclose"></param>
        /// <param name="discord"></param>
        /// <param name="dataStore"></param>
        void Init(IDiscloseSettings disclose, IDiscordCommands discord, IDataStore dataStore);
    }
}
