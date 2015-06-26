namespace ClientRepository
{
    using ClientRepository.Extractions;
    using ClientRepository.Model;
    using ClientRepository.Model.Interfaces;
    using DALs.Model;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using DALs.Sql;
    using Microsoft.Azure;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public class DbClient : IDbClient
    {
        #region Members
        /// <summary>
        /// The sprocs
        /// </summary>
        private readonly ISprocs sprocs;

        /// <summary>
        /// The connection string
        /// </summary>
        private static readonly string ConnectionString = CloudConfigurationManager.GetSetting(Settings.DbConnectionString);
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DbClient"/> class.
        /// </summary>
        public DbClient(): this(new Sprocs())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbClient"/> class.
        /// </summary>
        /// <param name="sprocs">The sprocs.</param>
        /// <exception cref="System.ArgumentNullException">sprocs</exception>
        public DbClient(ISprocs sprocs)
        {
            if (sprocs == null)
            {
                throw new ArgumentNullException("sprocs");
            }
            this.sprocs = sprocs;
        }
        #endregion

        #region Logics
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;Ad&gt;&gt;.</returns>
        public async Task<IEnumerable<Ad>> GetAsync()
        {
            var config = new SqlSprocConfiguration
            {
                ConnectionString = ConnectionString,
                Mode = SprocMode.ExecuteReader,
                SqlParameters = null,
                StoredProcedureName = Settings.GetAd
            };
            return await sprocs.ExecuteAsync(config, AdsLoader);
        }
        #endregion

        #region Loaders
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
        #endregion
    }
}
