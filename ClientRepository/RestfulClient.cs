namespace ClientRepository
{
    using ClientRepository.Model;
    using ClientRepository.Model.Interfaces;
    using DALs.Model.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Class RestfulClient.
    /// </summary>
    public class RestfulClient: IRestfulClient
    {
        #region Members
        /// <summary>
        /// The web client
        /// </summary>
        private readonly IWebClient webClient;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// </summary>
        public RestfulClient(): this(new HttpRepository.WebClient())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestfulClient"/> class.
        /// </summary>
        /// <param name="webClient">The web client.</param>
        public RestfulClient(IWebClient webClient)
        {
            this.webClient = webClient;
        }
        #endregion

        #region Logics
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<IEnumerable<Ad>> GetAsync(IEnumerable<long> ids)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Loaders
        #endregion
    }
}
