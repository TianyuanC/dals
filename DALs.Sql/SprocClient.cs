namespace DALs.Sql
{
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using DALs.Sql.Extensions;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Sprocs.
    /// </summary>
    public class SprocClient : ISprocClient
    {
        public virtual async Task<int> CommandAsync(SqlSprocConfiguration config)
        {
            int result = -1;
            using (var connection = new SqlConnection(config.ConnectionString))
            {
                try
                {
                    using (var command = new SqlCommand(config.StoredProcedureName, connection))
                    {
                        command.LoadParameters(config.SqlParameters.ToList());
                        connection.Open();
                        result = await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.StackTrace);
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return result;
        }

        public virtual async Task<T> QueryAsync<T>(SqlSprocConfiguration config, Func<IDataReader, T> loader)
        {
            T result = default(T);
            using (var connection = new SqlConnection(config.ConnectionString))
            {
                try
                {
                    using (var command = new SqlCommand(config.StoredProcedureName, connection))
                    {
                        command.LoadParameters(config.SqlParameters.ToList());
                        connection.Open();
                        switch (config.Mode)
                        {
                            case SprocMode.ExecuteReader:
                                using (var reader = await command.ExecuteReaderAsync())
                                {
                                    result = loader(reader);
                                }
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
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            return result;
        }
    }
}
