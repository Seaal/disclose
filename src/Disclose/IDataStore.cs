using System.Threading.Tasks;

namespace Disclose
{
    /// <summary>
    /// Persists data for Disclose handlers.
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Returns data for a server stored against the given key.
        /// </summary>
        /// <typeparam name="TData">The data type that is returned.</typeparam>
        /// <param name="server">The server to return data for.</param>
        /// <param name="key">The key to return data for.</param>
        /// <returns>A Task representing the data stored against the given server/key.</returns>
        Task<TData> GetServerDataAsync<TData>(DiscloseServer server, string key);

        /// <summary>
        /// Adds/Updates data for a server using the given key.
        /// </summary>
        /// <typeparam name="TData">The data type of the data being stored.</typeparam>
        /// <param name="server">The server to set the data for.</param>
        /// <param name="key">The key to set the data for.</param>
        /// <param name="data">The data to store for the given server/key.</param>
        /// <returns>A Task representing the completion of the operation.</returns>
        Task SetServerDataAsync<TData>(DiscloseServer server, string key, TData data);

        /// <summary>
        /// Returns data for a user stored against the given key.
        /// </summary>
        /// <typeparam name="TData">The type of the data that is returned.</typeparam>
        /// <param name="user">The data to return data for.</param>
        /// <param name="key">The key to return data for.</param>
        /// <returns>A Task representing the data stored against the given user/key.</returns>
        Task<TData> GetUserDataAsync<TData>(DiscloseUser user, string key);

        /// <summary>
        /// Adds/Updates data for a user using the given key.
        /// </summary>
        /// <typeparam name="TData">The data type of the data being stored.</typeparam>
        /// <param name="user">The user to set the data for.</param>
        /// <param name="key">The key to set the data for.</param>
        /// <param name="data">The data to store for the given server/key.</param>
        /// <returns>A Task representing the completion of the operation.</returns>
        Task SetUserDataAsync<TData>(DiscloseUser user, string key, TData data);
    }
}
