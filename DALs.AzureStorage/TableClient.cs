using System.Threading.Tasks;
using DALs.Model.Interfaces;

namespace DALs.AzureStorage
{
    /// <summary>
    /// Class TableClient.
    /// </summary>
    public class TableClient: ITableClient
    {
        public TableClient()
        {
        }

        public Task QueryAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
