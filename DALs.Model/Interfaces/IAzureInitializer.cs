using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace DALs.Model.Interfaces
{
    /// <summary>
    /// Interface IAzureInitializer
    /// </summary>
    public interface IAzureInitializer
    {
        /// <summary>
        /// Tables the client.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>CloudTable.</returns>
        CloudTable Table(string tableName);
    }
}
