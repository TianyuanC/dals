namespace DALs.Model.Configs
{
    using Enums;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    /// <summary>
    /// Class SqlSprocConfiguration.
    /// </summary>
    public class SqlSprocConfiguration
    {
        public SqlSprocConfiguration(string connectionString, string sprocName, SprocMode mode)
        {
            ConnectionString = connectionString;
            StoredProcedureName = sprocName;
            Mode = mode;
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>The name of the stored procedure.</value>
        public string StoredProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the SQL parameters.
        /// </summary>
        /// <value>The SQL parameters.</value>
        public IEnumerable<SqlParameter> SqlParameters { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public SprocMode Mode { get; set; }
    }
}
