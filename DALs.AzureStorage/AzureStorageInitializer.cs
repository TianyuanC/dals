using DALs.Model.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace DALs.AzureStorage
{
    /// <summary>
    /// Class AzureStorageInitializer.
    /// </summary>
    public class AzureStorageInitializer : IAzureInitializer
    {
        /// <summary>
        /// The storage account
        /// </summary>
        private readonly CloudStorageAccount storageAccount;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageInitializer"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public AzureStorageInitializer(string connectionString)
            : this(CloudStorageAccount.Parse(connectionString))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageInitializer" /> class.
        /// </summary>
        /// <param name="storageAccount">The storage account.</param>
        public AzureStorageInitializer(CloudStorageAccount storageAccount)
        {
            this.storageAccount = storageAccount;
        }

        /// <summary>
        /// Tables the client.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>CloudTable.</returns>
        public CloudTable Table(string tableName)
        {
            return storageAccount.CreateCloudTableClient().GetTableReference(tableName);
        }
    }
}
