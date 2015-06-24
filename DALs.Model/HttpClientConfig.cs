namespace DALs.Model
{
    using DALs.Model.Enums;
    using System;

    /// <summary>
    /// Class HttpClientConfig.
    /// </summary>
    public class HttpClientConfig
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        public string Route { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the request method.
        /// </summary>
        /// <value>The request method.</value>
        public HttpRequestMethod RequestMethod { get; set; }
    }
}
