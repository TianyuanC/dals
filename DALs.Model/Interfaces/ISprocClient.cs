namespace DALs.Model.Interfaces
{
    using System;
    using System.Data;
    using System.Threading.Tasks;
    using Configs;

    /// <summary>
    /// Interface ISprocs
    /// </summary>
    public interface ISprocClient
    {
        Task<int> CommandAsync(SqlSprocConfiguration configuration);

        Task<T> QueryAsync<T>(SqlSprocConfiguration configuration, Func<IDataReader, T> loader);
    }
}
