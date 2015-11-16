using DALs.AzureStorage;
using Microsoft.WindowsAzure.Storage;
using NUnit.Framework;

namespace DALs.UnitTests.DALsAzure
{
    [TestFixture]
    public class AzureInitializerTests
    {
        [Test]
        public void Constructor()
        {
            new AzureStorageInitializer("UseDevelopmentStorage=true");
        }

        [Test]
        public void ConstructorStorageAccount()
        {
            new AzureStorageInitializer(CloudStorageAccount.DevelopmentStorageAccount);
        }

        [Test]
        public void TableClient()
        {
            var init = new AzureStorageInitializer(CloudStorageAccount.DevelopmentStorageAccount);
            init.Table("test");
        }
    }
}
