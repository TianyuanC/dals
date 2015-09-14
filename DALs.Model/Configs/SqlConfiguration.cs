namespace DALs.Model.Configs
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// Class SqlSprocConfiguration.
    /// </summary>
    public class SqlConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConfiguration"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="sprocName">Name of the sproc.</param>
        /// <param name="mode">The mode.</param>
        public SqlConfiguration(string connectionString, string sprocName, SprocMode mode = SprocMode.ExecuteNonQuery)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (string.IsNullOrEmpty(sprocName))
            {
                throw new ArgumentNullException("sprocName");
            }

            ConnectionString = connectionString;
            StoredProcedures = new List<string> { sprocName };
            Mode = mode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlConfiguration"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="sprocNames">Name of the sproc.</param>
        /// <param name="mode">The mode.</param>
        public SqlConfiguration(string connectionString, ICollection<string> sprocNames, SprocMode mode = SprocMode.ExecuteNonQuery)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (sprocNames == null || !sprocNames.Any())
            {
                throw new ArgumentNullException("sprocNames");
            }

            ConnectionString = connectionString;
            StoredProcedures = sprocNames;
            Mode = mode;
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>The name of the stored procedure.</value>
        public IEnumerable<string> StoredProcedures { get; private set; }

        /// <summary>
        /// Gets or sets the SQL parameters.
        /// </summary>
        /// <value>The SQL parameters.</value>
        public IEnumerable<SqlParameter> SqlParameters { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public SprocMode Mode { get; private set; }
    }
}
