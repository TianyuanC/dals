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
            new InitHttpHelper();
        }

        [Test]
        public void HttpClient()
        {
            var provider = new InitHttpHelper();
            Assert.IsTrue(provider.HttpClient is HttpClient);
        }
    }
}
