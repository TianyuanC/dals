namespace DALs.Model.Interfaces
{
    using System.Net.Http;

    /// <summary>
    /// Interface IInitProvider
    /// </summary>
    public interface IInitHttpHelper
    {
        /// <summary>
        /// Gets the HTTP client.
        /// </summary>
        /// <value>The HTTP client.</value>
        HttpClient HttpClient { get; }
    }
}
