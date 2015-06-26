namespace ClientRepository.IntegrationTests
{
    using DALs.Http;
    using DALs.Model;
    using DALs.Model.Interfaces;
    using DALs.Sql;
    using System.Threading.Tasks;

    /// <summary>
    /// Class NugetTest.
    /// </summary>
    public class NugetTest
    {
        /// <summary>
        /// The sprocs
        /// </summary>
        private readonly ISprocs sprocs;
        /// <summary>
        /// The web client
        /// </summary>
        private readonly IWebClient webClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetTest"/> class.
        /// </summary>
        public NugetTest():this(new Sprocs(), new WebClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetTest"/> class.
        /// </summary>
        /// <param name="sprocs">The sprocs.</param>
        /// <param name="webClient">The web client.</param>
        public NugetTest(ISprocs sprocs, IWebClient webClient)
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
            var config = new SqlSprocConfiguration();
            await sprocs.ExecuteAsync(config, (dataReader) => 1);
            var httpConfig = new HttpClientConfig();
            await webClient.GetAsync(httpConfig, (response) => 1);
        }
    }
}
