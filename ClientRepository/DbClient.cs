namespace ClientRepository
{
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using DALs.Sql;
    using DALs.Sql.Extractions;
    using Microsoft.Azure;
    using Model;
    using Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public class DbClient : IDbClient
    {
        /// <summary>
        /// The sqlClient
        /// </summary>
        private readonly ISqlClient sqlClient;

        /// <summary>
        /// The connection string
        /// </summary>
        private static readonly string ConnectionString = CloudConfigurationManager.GetSetting(Settings.DbConnectionString);

        /// <summary>
        /// Initializes a new instance of the <see cref="DbClient"/> class.
        /// </summary>
        public DbClient(): this(new SqlClient())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbClient"/> class.
        /// </summary>
        /// <param name="sqlClient">The sqlClient.</param>
        /// <exception cref="System.ArgumentNullException">sqlClient</exception>
        public DbClient(ISqlClient sqlClient)
        {
            if (sqlClient == null)
            {
                throw new ArgumentNullException("sqlClient");
            }
            this.sqlClient = sqlClient;
        }

        /// <summary>
        /// Gets ads.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
        public async Task<IEnumerable<Ad>> GetAsync()
        {
            var config = new SqlConfiguration(ConnectionString, Settings.GetAd, SprocMode.ExecuteReader);
            return await sqlClient.QueryAsync(config, AdsLoader);
        }

        /// <summary>
        /// Insert new ad.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> InsertAsync()
        {
            SqlParameter content = new SqlParameter("content", SqlDbType.VarChar) { Value = "Test" };
            var config = new SqlConfiguration(ConnectionString, "InsertAd") { SqlParameters = new List<SqlParameter> { content } };
            return await sqlClient.CommandAsync(config) > 0;
        }

        /// <summary>
        /// update ad.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> UpdateAsync()
        {
            var content = new SqlParameter("content", SqlDbType.VarChar) { Value = "Test" };
            var id = new SqlParameter("id", SqlDbType.Int) { Value = 1 };
            var config = new SqlConfiguration(ConnectionString, "UpdateAd") { SqlParameters = new List<SqlParameter> { content, id } };
            return await sqlClient.CommandAsync(config) > 0;
        }

        /// <summary>
        /// Ads loader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>IEnumerable&lt;Ad&gt;.</returns>
        public static IEnumerable<Ad> AdsLoader(IDataReader reader)
        {
            var ads = new List<Ad>();

            while (reader.Read())
            {
                var adId = reader.Get<int>("ID");
                var lastMod = reader.Get<DateTime>("ModificationDate");
                var content = reader.Get<string>("Content");
                var counter = reader.Get<long>("Counter");
                var ad = new Ad
                {
                    AdID = adId,
                    LastModificationDate = lastMod,
                    Content = content,
                    TestCounter = counter
                };
                ads.Add(ad);

            }
            return ads;
        }
    }
}
