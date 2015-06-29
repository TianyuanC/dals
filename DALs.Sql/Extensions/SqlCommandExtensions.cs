namespace DALs.Sql.Extensions
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// Class SqlCommandExtensions.
    /// </summary>
    public static class SqlCommandExtensions
    {
        /// <summary>
        /// Loads the parameters.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="parameters">The parameters.</param>
        public static void LoadParameters(this IDbCommand command, List<SqlParameter> parameters)
        {
            command.CommandType = CommandType.StoredProcedure;
            IEnumerable<SqlParameter> sqlParameters = parameters;

            if (sqlParameters != null && sqlParameters.Any())
            {
                foreach (var sqlParameter in parameters)
                {
                    command.Parameters.Add(sqlParameter);
                }
            }
        }
    }
}
