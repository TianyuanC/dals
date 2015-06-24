namespace HttpRepository
{
    using HttpRepository.Model;
    using HttpRepository.Model.Interfaces;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Class WebClient.
    /// </summary>
    public class WebClient : IWebClient
    {
        public async Task<T> GetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T> loader)
        {
            if (null == config.Uri)
            {
                throw new ArgumentNullException("Uri");
            }
            if (string.IsNullOrWhiteSpace(config.Route))
            {
                throw new ArgumentException("route");
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
    }
}
