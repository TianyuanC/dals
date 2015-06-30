namespace DALs.Http
{
    using DALs.Model.Interfaces;
    using System;
    using System.Net.Http;

    /// <summary>
    /// Class HttpInitializer.
    /// </summary>
    public class HttpInitializer : IHttpInitializer
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly Lazy<HttpClient> httpClient 
            = new Lazy<HttpClient>(()=>new HttpClient());
        
        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <value>The HTTP client.</value>
        public HttpClient HttpClient {
            get { return httpClient.Value; }
        }
    }
}
