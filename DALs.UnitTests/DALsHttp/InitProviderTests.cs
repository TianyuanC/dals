namespace DALs.UnitTests.DALsHttp
{
    using DALs.Http;
    using NUnit.Framework;
    using System.Net.Http;

    [TestFixture]
    public class InitProviderTests
    {
        [Test]
        public void Constructor()
        {
            new InitProvider();
        }

        [Test]
        public void HttpClient()
        {
            var provider = new InitProvider();
            Assert.IsTrue(provider.HttpClient is HttpClient);
        }
    }
}
