namespace DALs.AzureStorage
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;
    using Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class QueueClient.
    /// </summary>
    public class QueueClient : IQueueClient
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly CloudQueueClient client;

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        public CloudQueueClient Client 
        {
            get
            {
                return client;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueClient"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public QueueClient(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            client = storageAccount.CreateCloudQueueClient(); 
        }

        /// <summary>
        /// peek as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="loader">The loader.</param>
        /// <param name="top">The top.</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        public virtual async Task<IEnumerable<T>> PeekAsync<T>(string queueName, Func<CloudQueueMessage, T> loader, int top = 32)
        {
            CloudQueue queue = client.GetQueueReference(queueName);
            var msgs = await queue.PeekMessagesAsync(top);
            return msgs.Select(loader);
        }

        /// <summary>
        /// get information as an asynchronous operation.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns>Task&lt;CloudQueue&gt;.</returns>
        public virtual async Task<CloudQueue> GetInfoAsync(string queueName)
        {
            CloudQueue queue = client.GetQueueReference(queueName);
            await queue.FetchAttributesAsync();
            return queue;
        }
    }
}
