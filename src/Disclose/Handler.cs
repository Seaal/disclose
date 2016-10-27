using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    /// <summary>
    /// A base class for all Handlers. Extend this to create your own Handlers.
    /// </summary>
    public abstract class Handler<T> : IHandler<T> where T : class, IHandler<T>
    {
        protected IDiscloseSettings Disclose { get; private set; }

        protected IDiscordCommands Discord { get; private set; }

        protected IDataStore DataStore { get; private set; }

        public Func<IUser, bool> UserFilter { get; private set; }

        /// <inheritdoc />
        public virtual void Init(IDiscloseSettings disclose, IDiscordCommands discord, IDataStore dataStore)
        {
            Disclose = disclose;
            Discord = discord;
            DataStore = dataStore;
        }

        /// <inheritdoc />
        public T RestrictToUsers(Func<IUser, bool> user)
        {
            UserFilter = user;

            return this as T;
        }
    }
}
