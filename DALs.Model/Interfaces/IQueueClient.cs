namespace DALs.Model.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System;
    using Microsoft.WindowsAzure.Storage.Queue;

    public interface IQueueClient
    {
        Task<IEnumerable<T>> PeekAsync<T>(string queueName, Func<CloudQueueMessage, T> loader, int top = 32);
        Task<CloudQueue> GetInfoAsync(string queueName);
    }
}
