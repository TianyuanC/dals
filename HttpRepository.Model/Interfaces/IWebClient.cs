namespace HttpRepository.Model.Interfaces
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IWebClient
    {
        Task<T> GetAsync<T>(HttpClientConfig config, Func<HttpResponseMessage, T>loader );
    }
}
