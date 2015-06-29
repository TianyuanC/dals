namespace DALs.Http
{
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Class WebClient.
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// The inits
        /// </summary>
        private readonly IInitHttpHelper inits;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        public RestClient():this(new InitHttpHelper())
        {  
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="initProvider">The initialize provider.</param>
        public RestClient(IInitHttpHelper initProvider)
        {
            if (initProvider == null)
            {
                throw new ArgumentNullException("initProvider");
            }
            inits = initProvider;
        }

        /// <summary>
        /// get as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public virtual async Task<T> GetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T> loader)
        {
            if (null == config.Uri)
            {
                throw new ArgumentException("Uri");
            }
            if (string.IsNullOrWhiteSpace(config.Route))
            {
                throw new ArgumentException("Route");
            }

            using (var client = inits.HttpClient)
            {
                client.BaseAddress = config.Uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(config.Route);
                return loader(response);
            }
        }

        /// <summary>
        /// set as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public virtual async Task<T> SetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T> loader)
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
                throw new ArgumentException("Data");
            }
            if (config.RequestMethod != HttpRequest.Post && config.RequestMethod != HttpRequest.Put)
            {
                throw new ArgumentException("RequestMethod");
            }

            using (var client = inits.HttpClient)
            {
                client.BaseAddress = config.Uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = null;
                switch (config.RequestMethod)
                {
                    case HttpRequest.Put:
                        response = await client.PutAsJsonAsync(config.Route, config.Data);
                    break;
                    case HttpRequest.Post:
                        response = await client.PostAsJsonAsync(config.Route, config.Data);
                    break;
                }
                return loader(response);
            }
        }
    }
}
