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

            while (reader.Read())
            {
                var adId = reader.Get<int>("AdID");
                var lastMod = reader.Get<DateTime>("LastModificationDate");
                var content = reader.Get<string>("Content");
                var counter = reader.Get<long>("TestCounter");
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
