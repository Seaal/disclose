using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;
using Disclose.DiscordClient.DiscordNetAdapters;

namespace Disclose
{
    public interface IHandler<T> where T: IHandler<T>
    {
        /// <summary>
        /// Called when the command handler is registered, so you have access to disclose's services.
        /// </summary>
        /// <param name="disclose"></param>
        /// <param name="discord"></param>
        /// <param name="dataStore"></param>
        void Init(IDiscloseSettings disclose, IDiscordCommands discord, IDataStore dataStore);

        Func<IUser, bool> UserFilter { get; }

        /// <summary>
        /// Restrict this handler to only run for certain users.
        /// </summary>
        /// <param name="role"></param>
        /// <returns>The handler.</returns>
        T RestrictToUsers(Func<IUser, bool> role);
    }
}
