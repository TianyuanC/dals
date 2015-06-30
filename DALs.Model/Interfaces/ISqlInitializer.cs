namespace DALs.Model.Interfaces
{
    using System.Data;

    /// <summary>
    /// Interface ISqlInitializer
    /// </summary>
    public interface ISqlInitializer
    {
        /// <summary>
        /// Databases the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>IDbConnection.</returns>
        IDbConnection DbConnection(string connectionString);

        /// <summary>
        /// Databases the command.
        /// </summary>
        /// <param name="sproc">The sproc.</param>
        /// <returns>IDbCommand.</returns>
        IDbCommand DbCommand(string sproc);
    }
}
