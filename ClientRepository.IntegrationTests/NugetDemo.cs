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
    /// Class NugetDemo.
    /// </summary>
    public class NugetDemo
    {
        /// <summary>
        /// The sprocs
        /// </summary>
        private readonly ISprocClient sprocs;
        /// <summary>
        /// The web client
        /// </summary>
        private readonly IRestClient restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetDemo"/> class.
        /// </summary>
        public NugetDemo():this(new SprocClient(), new RestClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetDemo"/> class.
        /// </summary>
        /// <param name="sprocs">The sprocs.</param>
        /// <param name="restClient">The web client.</param>
        public NugetDemo(ISprocClient sprocs, IRestClient restClient)
        {
            this.sprocs = sprocs;
            this.restClient = restClient;
        }

        /// <summary>
        /// Test this instance.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Demo()
        {
            var sqlQueryConfig = new SprocConfiguration("connection", "sproc", SprocMode.ExecuteReader);
            await sprocs.QueryAsync(sqlQueryConfig, reader => 1);

            var sqlCommandConfig = new SprocConfiguration("connection", "sproc", SprocMode.ExecuteNonQuery);
            await sprocs.CommandAsync(sqlCommandConfig);

            var httpGetConfig = new HttpConfiguration(new Uri("http://test.ca"), "route", HttpRequest.Get);
            await restClient.GetAsync(httpGetConfig, response => 1);

            var httpSetConfig = new HttpConfiguration(new Uri("http://test.ca"), "route", HttpRequest.Put) { Data = "test" };
            await restClient.GetAsync(httpSetConfig, response => 1);
        }
    }
}
