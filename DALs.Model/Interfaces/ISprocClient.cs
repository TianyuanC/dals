namespace DALs.Model.Interfaces
{
    using Configs;
    using System;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface ISprocs
    /// </summary>
    public interface ISprocClient
    {
        /// <summary>
        /// Commands the asynchronous.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        Task<int> CommandAsync(SqlConfiguration configuration);

        /// <summary>
        /// Queries the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> QueryAsync<T>(SqlConfiguration configuration, Func<IDataReader, T> loader);
    }
}
