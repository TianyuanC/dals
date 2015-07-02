namespace DALs.UnitTests.DALsHttp
{
    using DALs.Http;
    using NUnit.Framework;
    using System.Net.Http;

    [TestFixture]
    public class HttpInitializerTests
    {
        [Test]
        public void Constructor()
        {
            new HttpInitializer();
        }

        [Test]
        public void HttpClient()
        {
            var provider = new HttpInitializer();
            Assert.IsTrue(provider.HttpClient is HttpClient);
        }
    }
}
