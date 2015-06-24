namespace DbRepository
{
    using DALs.Model;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Sprocs.
    /// </summary>
    public class Sprocs : ISprocs
    {
        /// <summary>
        /// execute sproc as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public async Task<T> ExecuteAsync<T>(SqlSprocConfiguration config, Func<IDataReader, T> loader)
        {
            T result = default(T);
            using (var connection = new SqlConnection(config.ConnectionString))
            {
                try
                {
                    using (var command = new SqlCommand(config.StoredProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        IEnumerable<SqlParameter> sqlParameters = config.SqlParameters;

                        if (sqlParameters != null && sqlParameters.Any())
                        {
                            command.Parameters.AddRange(sqlParameters.ToArray());
                        }

                        connection.Open();

                        switch (config.Mode)
                        {
                            case SprocMode.ExecuteReader:
                                using (var reader = await command.ExecuteReaderAsync())
                                {
                                    result = loader(reader);
                                }
                                break;
                            case SprocMode.ExecuteNonQuery:
                                await command.ExecuteNonQueryAsync();
                                break;
                            case SprocMode.ExecuteScalar:
                                result = (T)await command.ExecuteScalarAsync();
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.StackTrace);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return result;
        }
    }
}
