namespace DALs.Model.Configs
{
    using DALs.Model.Enums;
    using System;

    /// <summary>
    /// Class HttpClientConfig.
    /// </summary>
    public class HttpConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpConfiguration"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="route">The route.</param>
        /// <param name="method">The method.</param>
        public HttpConfiguration(Uri uri, string route, HttpRequest method)
        {
            this.Uri = uri;
            this.Route = route;
            this.RequestMethod = method;
        }

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
        public HttpRequest RequestMethod { get; set; }
    }
}
