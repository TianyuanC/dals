namespace ClientRepository.Model.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IDbClient
    /// </summary>
    public interface IDbClient
    {
        Task<IEnumerable<Ad>> Get();
    }
}
