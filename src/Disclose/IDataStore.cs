using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface IDataStore
    {
        Task<TData> GetServerData<TData>(IServer server, string key);
        Task SetServerData<TData>(IServer server, string key, TData data);

        Task<TData> GetUserData<TData>(IUser user, string key);
        Task SetUserData<TData>(IUser user, string key, TData data);

        Task<TData> GetUserDataForServer<TData>(IServer server, IUser user, string key, TData data);
        Task SetUserData<TData>(IServer server, IUser user, string key, TData data);
    }
}
