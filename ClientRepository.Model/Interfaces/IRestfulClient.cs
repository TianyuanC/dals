namespace ClientRepository.Model.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IRestfulClient
    /// </summary>
    public interface IRestfulClient
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
        Task<IEnumerable<Ad>> GetAsync(IEnumerable<long> ids);
    }
}
