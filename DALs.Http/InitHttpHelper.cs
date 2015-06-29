namespace DALs.Http
{
    using DALs.Model.Interfaces;
    using System;
    using System.Net.Http;

    /// <summary>
    /// Class InitHttpHelper.
    /// </summary>
    public class InitHttpHelper : IInitHttpHelper
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private Lazy<HttpClient> httpClient = new Lazy<HttpClient>(()=>new HttpClient());
        
        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <value>The HTTP client.</value>
        public HttpClient HttpClient {
            get { return httpClient.Value; }
        }
    }
}
