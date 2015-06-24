namespace DbRepository.Model.Interfaces
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface ISprocs
    /// </summary>
    public interface ISprocs
    {
        /// <summary>
        /// Executes the sproc asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> ExecuteAsync<T>(SqlSprocConfiguration configuration, Func<IDataReader, T> loader);
    }
}
