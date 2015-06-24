namespace DALs.Model.Interfaces
{
    using DALs.Model;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IWebClient
    /// </summary>
    public interface IWebClient
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> GetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T> loader );

        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> SetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T> loader);
    }
}
