namespace DALs.AzureStorage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Model.Interfaces;
    using System.Threading.Tasks;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;

    public class QueueClient : IQueueClient
    {
        private readonly CloudQueueClient client;

        public CloudQueueClient Client 
        {
            get
            {
                return client;
            }
        }

        public QueueClient(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            client = storageAccount.CreateCloudQueueClient(); 
        }

        public virtual async Task<IEnumerable<T>> PeekAsync<T>(string queueName, Func<CloudQueueMessage, T> loader, int top = 32)
        {
            CloudQueue queue = client.GetQueueReference(queueName);
            var msgs = await queue.PeekMessagesAsync(top);
            return msgs.Select(loader);
        }

        public virtual async Task<CloudQueue> GetInfoAsync(string queueName)
        {
            CloudQueue queue = client.GetQueueReference(queueName);
            await queue.FetchAttributesAsync();
            return queue;
        }
    }
}
