﻿namespace DALs.Model.Interfaces
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using DALs.Model.Configs;

    /// <summary>
    /// Interface ISprocs
    /// </summary>
    public interface ISprocClient
    {
        /// <summary>
        /// Executes the sproc asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> ExecuteAsync<T>(SqlSprocConfiguration configuration, Func<IDataReader, T> loader);
    }
}