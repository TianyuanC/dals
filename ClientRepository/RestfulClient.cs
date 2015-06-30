namespace ClientRepository
{
    using ClientRepository.Model;
    using ClientRepository.Model.Interfaces;
    using DALs.Http;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using Microsoft.Azure;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Class RestfulClient.
    /// </summary>
    public class RestfulClient: IRestfulClient
    {
        /// <summary>
        /// The web client
        /// </summary>
        private readonly IRestClient restClient;
        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// </summary>
        public RestfulClient(): this(new RestClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// </summary>
        /// <param name="restClient">The web client.</param>
        public RestfulClient(IRestClient restClient)
        {
            if (restClient == null)
            {
                throw new ArgumentNullException("restClient");
            }
            this.restClient = restClient;
        }

        /// <summary>
        /// get as an asynchronous operation.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
        public async Task<IEnumerable<Ad>> GetAsync(IEnumerable<long> ids)
        {
            var uri = new Uri(CloudConfigurationManager.GetSetting(Settings.TestApiUri));
            var config = new HttpConfiguration(uri, "api/ad", HttpRequest.Get);
            return await this.restClient.GetAsync(config, LoadAds);
        }

        /// <summary>
        /// Loads the ads.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>IEnumerable&lt;Ad&gt;.</returns>
        public static IEnumerable<Ad> LoadAds(HttpResponseMessage msg)
        {
            return new List<Ad>();
        }
    }
}
