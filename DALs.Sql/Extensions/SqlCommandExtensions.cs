using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DALs.Sql.Extensions
{
    public static class SqlCommandExtensions
    {
        public static void LoadParameters(this SqlCommand command, List<SqlParameter> parameters)
        {
            command.CommandType = CommandType.StoredProcedure;
            IEnumerable<SqlParameter> sqlParameters = parameters;

            if (sqlParameters != null && sqlParameters.Any())
            {
                command.Parameters.AddRange(sqlParameters.ToArray());
            }
        }
    }
}
