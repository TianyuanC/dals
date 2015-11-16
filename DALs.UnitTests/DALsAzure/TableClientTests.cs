using DALs.AzureStorage;
using DALs.Model.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace DALs.UnitTests.DALsAzure
{
    [TestFixture]
    public class TableClientTests
    {
        [Test]
        public void Constructor()
        {
            new TableClient("UseDevelopmentStorage=true", "Test");
        }

        [Test]
        public void ConstructorInit()
        {
            var init = Substitute.For<IAzureInitializer>();
            new TableClient(init, "Test");
        }
    }
}
