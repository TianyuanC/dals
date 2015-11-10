using System.Net.Http;
using System.Threading.Tasks;

namespace DALs.Http.Conversion
{
    /// <summary>
    /// Class HttpResponseMessageConversion.
    /// </summary>
    public static class HttpResponseMessageConversion
    {
        /// <summary>
        /// To the specified response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">The response.</param>
        /// <returns>T.</returns>
        public static T To<T>(this HttpResponseMessage response)
        {
            Task<T> readTask = response.Content.ReadAsAsync<T>();
            readTask.Wait();
            return readTask.Result;
        }
    }
}
