using System;
using Disclose.DiscordClient;

namespace Disclose
{
    /// <summary>
    /// A base class for all Handlers. Extend this to create your own Handlers.
    /// </summary>
    public abstract class Handler<T> : IHandler<T> where T : class, IHandler<T>
    {
        protected IDiscloseFacade Disclose { get; private set; }

        protected IDataStore DataStore { get; private set; }

        public Func<DiscloseUser, bool> UserFilter { get; private set; }

        /// <inheritdoc />
        public virtual void Init(IDiscloseFacade disclose, IDataStore dataStore)
        {
            Disclose = disclose;
            DataStore = dataStore;
        }

        /// <inheritdoc />
        public T RestrictToUsers(Func<DiscloseUser, bool> user)
        {
            UserFilter = user;

            return this as T;
        }
    }
}
