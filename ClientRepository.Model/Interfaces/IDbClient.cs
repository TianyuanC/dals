﻿namespace ClientRepository.Model.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IDbClient
    /// </summary>
    public interface IDbClient
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
        Task<IEnumerable<Ad>> GetAsync();
    }
}
