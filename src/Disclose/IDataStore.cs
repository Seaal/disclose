using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface IDataStore
    {
        TData GetServerData<TData>(IServer server, string key);
        void SetServerData<TData>(IServer server, string key, TData data);

        TData GetUserData<TData>(IUser user, string key);
        void SetUserData<TData>(IUser user, string key, TData data);

        TData GetUserDataForServer<TData>(IServer server, IUser user, string key, TData data);
        void SetUserData<TData>(IServer server, IUser user, string key, TData data);
    }
}
