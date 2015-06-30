namespace DALs.Sql
{
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using DALs.Sql.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Sprocs.
    /// </summary>
    public class SprocClient : ISprocClient
    {
        /// <summary>
        /// The initialize
        /// </summary>
        private readonly ISqlInitializer init;

        /// <summary>
        /// Initializes a new instance of the <see cref="SprocClient"/> class.
        /// </summary>
        public SprocClient():this(new SqlInitializer())
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SprocClient"/> class.
        /// </summary>
        /// <param name="init">The initialize.</param>
        public SprocClient(ISqlInitializer init)
        {
            this.init = init;
        }

        /// <summary>
        /// command as an asynchronous operation.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public virtual async Task<int> CommandAsync(SprocConfiguration config)
        {
            int result = -1;
            using (IDbConnection connection = init.DbConnection(config.ConnectionString))
            {
                try
                {
                    using (IDbCommand command = init.DbCommand(config.StoredProcedureName))
                    {
                        command.LoadParameters(config.SqlParameters as List<SqlParameter>);
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
                    connection.ForceClose();
                }
            }
            return result;
        }

        /// <summary>
        /// query as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="loader">The loader.</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public virtual async Task<T> QueryAsync<T>(SprocConfiguration config, Func<IDataReader, T> loader = null)
        {
            T result = default(T);
            using (IDbConnection connection = init.DbConnection(config.ConnectionString))
            {
                try
                {
                    using (IDbCommand command = init.DbCommand(config.StoredProcedureName))
                    {
                        command.LoadParameters(config.SqlParameters as List<SqlParameter>);
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
                    connection.ForceClose();
                }
            }
            return result;
        }
    }
}
