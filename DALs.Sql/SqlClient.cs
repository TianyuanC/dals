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
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Sprocs.
    /// </summary>
    public class SqlClient : ISqlClient
    {
        /// <summary>
        /// The initialize
        /// </summary>
        private readonly ISqlInitializer init;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlClient"/> class.
        /// </summary>
        public SqlClient():this(new SqlInitializer())
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlClient"/> class.
        /// </summary>
        /// <param name="init">The initialize.</param>
        public SqlClient(ISqlInitializer init)
        {
            this.init = init;
        }

        /// <summary>
        /// command as an asynchronous operation.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public virtual async Task<int> CommandAsync(SqlConfiguration config)
        {
            int result = -1;
            using (IDbConnection connection = init.DbConnection(config.ConnectionString))
            {
                try
                {
                    using (IDbCommand command = init.DbCommand(config.StoredProcedures.First()))
                    {
                        command.Connection = connection;
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
        /// Execute multiple commands asynchronously.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="isolate">The isolation level.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public async Task<int> CommandMultipleAsync(SqlConfiguration config, IsolationLevel isolate = IsolationLevel.Unspecified)
        {
            int result = -1;
            
            using (IDbConnection connection = init.DbConnection(config.ConnectionString))
            {
                try
                {
                    using (IDbTransaction transaction = connection.BeginTransaction(isolate))
                    {
                        foreach (var storedProcedure in config.StoredProcedures)
                        {
                            using (IDbCommand command = init.DbCommand(storedProcedure))
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;
                                command.LoadParameters(config.SqlParameters as List<SqlParameter>);
                                connection.Open();
                                result = await command.ExecuteNonQueryAsync();
                            } 
                        }
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError(e.StackTrace);
                    result = -1;
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
        public virtual async Task<T> QueryAsync<T>(SqlConfiguration config, Func<IDataReader, T> loader = null)
        {
            T result = default(T);
            using (IDbConnection connection = init.DbConnection(config.ConnectionString))
            {
                try
                {
                    using (IDbCommand command = init.DbCommand(config.StoredProcedures.First()))
                    {
                        command.Connection = connection;
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
