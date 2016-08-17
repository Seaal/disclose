using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose
{
    public interface IDataStore
    {
        Task<TData> GetServerDataAsync<TData>(IServer server, string key);
        Task SetServerDataAsync<TData>(IServer server, string key, TData data);

        Task<TData> GetUserDataAsync<TData>(IUser user, string key);
        Task SetUserDataAsync<TData>(IUser user, string key, TData data);

        Task<TData> GetUserDataForServerAsync<TData>(IServer server, IUser user, string key, TData data);
        Task SetUserDataForServerAsync<TData>(IServer server, IUser user, string key);
    }
}
