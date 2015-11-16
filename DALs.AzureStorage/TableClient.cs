using System.Threading.Tasks;
using DALs.Model.Interfaces;
using Microsoft.WindowsAzure.Storage.Table;

namespace DALs.AzureStorage
{
    /// <summary>
    /// Class TableClient.
    /// </summary>
    public class TableClient: ITableClient
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly CloudTable client;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableClient"/> class.
        /// </summary>
        public TableClient(string connectionString, string tableName)
            : this(new AzureStorageInitializer(connectionString), tableName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableClient"/> class.
        /// </summary>
        /// <param name="init">The initialize.</param>
        /// <param name="tableName">Name of the table.</param>
        public TableClient(IAzureInitializer init, string tableName)
        {
            this.client = init.Table(tableName);
        }

        public Task QueryAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}
