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
        /// Gets this instance.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
        public async Task<IEnumerable<Ad>> GetAsync()
        {
            var config = new SqlConfiguration(ConnectionString, Settings.GetAd, SprocMode.ExecuteReader);
            return await sqlClient.QueryAsync(config, AdsLoader);
        }

        /// <summary>
        /// Ads loader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>IEnumerable&lt;Ad&gt;.</returns>
        public static IEnumerable<Ad> AdsLoader(IDataReader reader)
        {
            var ads = new List<Ad>();

            var idIndex = reader.GetOrdinal("Id");
            var lastModIndex = reader.GetOrdinal("LastModificationDate");
            var contentIndex = reader.GetOrdinal("Content");

            while (reader.Read())
            {
                var ad = new Ad
                {
                    Id = reader.Get<long>(idIndex),
                    LastModificationDate = reader.Get<DateTime>(lastModIndex),
                    Content = reader.Get<string>(contentIndex),
                };
                ads.Add(ad);

            }
            return ads;
        }
    }
}
