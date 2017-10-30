using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Disclose
{
    internal class DataStoreLockDecorator : IDataStore
    {
        public IDataStore DataStore { get; set; }
        private readonly ConcurrentDictionary<ulong, SemaphoreSlim> _locks;

        public DataStoreLockDecorator(IDataStore dataStore)
        {
            DataStore = dataStore;
            _locks = new ConcurrentDictionary<ulong, SemaphoreSlim>();
        }

        public async Task<TData> GetServerDataAsync<TData>(DiscloseServer server, string key)
        {
            SemaphoreSlim semaphore = _locks.GetOrAdd(server.Id, new SemaphoreSlim(1));

            await semaphore.WaitAsync();

            try
            {
                return await DataStore.GetServerDataAsync<TData>(server, key);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<TData> GetUserDataAsync<TData>(DiscloseUser user, string key)
        {
            SemaphoreSlim semaphore = _locks.GetOrAdd(user.Id, new SemaphoreSlim(1));

            await semaphore.WaitAsync();

            try
            {
                return await DataStore.GetUserDataAsync<TData>(user, key);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task SetServerDataAsync<TData>(DiscloseServer server, string key, TData data)
        {
            SemaphoreSlim semaphore = _locks.GetOrAdd(server.Id, new SemaphoreSlim(1));

            await semaphore.WaitAsync();

            try
            {
                await DataStore.SetServerDataAsync(server, key, data);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task SetUserDataAsync<TData>(DiscloseUser user, string key, TData data)
        {
            SemaphoreSlim semaphore = _locks.GetOrAdd(user.Id, new SemaphoreSlim(1));

            await semaphore.WaitAsync();

            try
            {
                await DataStore.SetUserDataAsync(user, key, data);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
