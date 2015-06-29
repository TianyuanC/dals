namespace DALs.Sql.Extensions
{
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;

    /// <summary>
    /// Class IDbCommandExtensions.
    /// </summary>
    public static class IDbCommandExtensions
    {
        /// <summary>
        /// execute reader as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>Task&lt;IDataReader&gt;.</returns>
        public static async Task<IDataReader> ExecuteReaderAsync(this IDbCommand command)
        {
            var dbCommand = command as DbCommand;
            if (dbCommand != null)
            {
                return await dbCommand.ExecuteReaderAsync();
            }
            return await Task.Run(() => command.ExecuteReader());
        }

        /// <summary>
        /// execute non query as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public static async Task<int> ExecuteNonQueryAsync(this IDbCommand command)
        {
            var dbCommand = command as DbCommand;
            if (dbCommand != null)
            {
                return await dbCommand.ExecuteNonQueryAsync();
            }
            return await Task.Run(() => command.ExecuteNonQuery());
        }

        /// <summary>
        /// execute scalar as an asynchronous operation.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>Task&lt;System.Object&gt;.</returns>
        public static async Task<object> ExecuteScalarAsync(this IDbCommand command)
        {
            var dbCommand = command as DbCommand;
            if (dbCommand != null)
            {
                return await dbCommand.ExecuteScalarAsync();
            }
            return await Task.Run(() => command.ExecuteScalar());
        }
    }
}
