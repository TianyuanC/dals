using System.Threading.Tasks;

namespace DALs.Model.Interfaces
{
    /// <summary>
    /// Interface ITableClient
    /// </summary>
    public interface ITableClient
    {
        /// <summary>
        /// Queries the asynchronous.
        /// </summary>
        /// <returns>Task.</returns>
        Task QueryAsync();

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <returns>Task.</returns>
        Task UpdateAsync<T>();
    }
}
