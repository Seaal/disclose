using System;
using Disclose.DiscordClient;

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
        void Init(IDiscloseFacade disclose, IDataStore dataStore);

        Func<DiscloseUser, bool> UserFilter { get; }

        /// <summary>
        /// Restrict this handler to only run for certain users.
        /// </summary>
        /// <param name="role"></param>
        /// <returns>The handler.</returns>
        T RestrictToUsers(Func<DiscloseUser, bool> role);
    }
}
