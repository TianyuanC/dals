namespace DALs.Model.Interfaces
{
    using Microsoft.WindowsAzure.Storage.Queue;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface IQueueClient
    /// </summary>
    public interface IQueueClient
    {
        /// <summary>
        /// Peeks the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName">Name of the queue.</param>
        /// <param name="loader">The loader.</param>
        /// <param name="top">The top.</param>
        /// <returns>Task&lt;IEnumerable&lt;T&gt;&gt;.</returns>
        Task<IEnumerable<T>> PeekAsync<T>(string queueName, Func<CloudQueueMessage, T> loader, int top = 32);
        
        /// <summary>
        /// Gets the information asynchronous.
        /// </summary>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns>Task&lt;CloudQueue&gt;.</returns>
        Task<CloudQueue> GetInfoAsync(string queueName);
    }
}
