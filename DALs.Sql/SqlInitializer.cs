namespace DALs.Sql
{
    using DALs.Model.Interfaces;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Class SqlInitializer.
    /// </summary>
    public class SqlInitializer : ISqlInitializer
    {
        /// <summary>
        /// Databases the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>IDbConnection.</returns>
        public IDbConnection DbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Databases the command.
        /// </summary>
        /// <param name="sproc">The sproc.</param>
        /// <returns>IDbCommand.</returns>
        public IDbCommand DbCommand(string sproc)
        {
            return new SqlCommand(sproc);
        }
    }
}
