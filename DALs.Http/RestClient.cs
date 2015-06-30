namespace DALs.Http
{
    using DALs.Http.Validation;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Class RestClient.
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// The inits
        /// </summary>
        private readonly IHttpInitializer inits;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        public RestClient():this(new HttpInitializer())
        {  
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient"/> class.
        /// </summary>
        /// <param name="init">The initialize provider.</param>
        public RestClient(IHttpInitializer init)
        {
            if (init == null)
            {
                throw new ArgumentNullException("init");
            }
            inits = init;
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
        public virtual async Task<T> GetAsync<T>(HttpConfiguration config, Func<HttpResponseMessage, T> loader)
        {
            config.ValidateGet();

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
        public virtual async Task<T> SetAsync<T>(HttpConfiguration config, Func<HttpResponseMessage, T> loader)
        {
            config.ValidateSet();

            using (HttpClient client = inits.HttpClient)
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
