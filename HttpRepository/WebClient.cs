namespace HttpRepository
{
    using DALs.Model;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Class WebClient.
    /// </summary>
    public class WebClient : IWebClient
    {
        /// <summary>
        /// get as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">Uri</exception>
        /// <exception cref="System.ArgumentException">route</exception>
        public async Task<T> GetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T> loader)
        {
            if (null == config.Uri)
            {
                throw new ArgumentException("Uri");
            }
            if (string.IsNullOrWhiteSpace(config.Route))
            {
                throw new ArgumentException("Route");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = config.Uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(config.Route);
                return loader(response);
            }
        }

        /// <summary>
        /// put as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentException">Uri
        /// or
        /// Route</exception>
        /// <exception cref="System.ArgumentNullException">Data</exception>
        public async Task<T> SetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T> loader)
        {
            if (null == config.Uri)
            {
                throw new ArgumentException("Uri");
            }
            if (string.IsNullOrWhiteSpace(config.Route))
            {
                throw new ArgumentException("Route");
            }
            if (null == config.Data)
            {
                throw new ArgumentNullException("Data");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = config.Uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = null;
                switch (config.RequestMethod)
                {
                        case HttpRequestMethod.Put:
                        response = await client.PutAsJsonAsync(config.Route, config.Data);
                        break;
                        case HttpRequestMethod.Post:
                        response = await client.PostAsJsonAsync(config.Route, config.Data);
                        break;
                }
                return loader(response);
            }
        }
    }
}
