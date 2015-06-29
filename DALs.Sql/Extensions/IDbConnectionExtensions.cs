namespace DALs.Sql.Extensions
{
    using System.Data;

    /// <summary>
    /// Class IDbConnectionExtensions.
    /// </summary>
    public static class IDbConnectionExtensions
    {
        /// <summary>
        /// Forces the close.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public static void ForceClose(this IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                return;
            }
            connection.Close();
            connection.Dispose();
        }
    }
}
