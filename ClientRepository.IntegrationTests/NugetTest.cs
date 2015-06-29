namespace ClientRepository.IntegrationTests
{
    using DALs.Http;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using DALs.Sql;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Class NugetTest.
    /// </summary>
    public class NugetTest
    {
        /// <summary>
        /// The sprocs
        /// </summary>
        private readonly ISprocClient sprocs;
        /// <summary>
        /// The web client
        /// </summary>
        private readonly IRestClient webClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetTest"/> class.
        /// </summary>
        public NugetTest():this(new SprocClient(), new RestClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetTest"/> class.
        /// </summary>
        /// <param name="sprocs">The sprocs.</param>
        /// <param name="webClient">The web client.</param>
        public NugetTest(ISprocClient sprocs, IRestClient webClient)
        {
            this.sprocs = sprocs;
            this.webClient = webClient;
        }

        /// <summary>
        /// Testses this instance.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Tests()
        {
            var config = new SqlSprocConfiguration("connection", "sproc", SprocMode.ExecuteReader);
            await sprocs.ExecuteAsync(config, (dataReader) => 1);
            var httpConfig = new HttpClientConfig(new Uri("uri"), "route", HttpRequest.Get);
            await webClient.GetAsync(httpConfig, (response) => 1);
        }
    }
}
