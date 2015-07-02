namespace DALs.Model.Interfaces
{
    using Configs;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface ISprocs
    /// </summary>
    public interface ISqlClient
    {
        /// <summary>
        /// Execute a single command asynchronously.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> CommandAsync(SqlConfiguration configuration);

        /// <summary>
        /// Execute multiple commands asynchronously.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="isolate">The isolation level.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> CommandMultipleAsync(SqlConfiguration configuration, IsolationLevel isolate = IsolationLevel.Unspecified);
        
        /// <summary>
        /// Execute a single query asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> QueryAsync<T>(SqlConfiguration configuration, Func<IDataReader, T> loader);
    }
}
