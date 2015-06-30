namespace DALs.UnitTests.DALsHttp
{
    using DALs.Http;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RestClientTests
    {
        [Test]
        public void Constructor()
        {
            new RestClient();
        }

        [Test]
        public void ConstructorNullInit()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new RestClient(null));
            Assert.AreEqual("init", ex.ParamName);
        }

        //[Test]
        //public void GetAsync()
        //{
        //    var init = Substitute.For<IInitProvider>();
        //    init.HttpClient.Returns(new HttpClient(new FakeMessageHandler()));
        //    var rest = new RestClient(init);
        //    //ready to mock
        //}

        //private class FakeMessageHandler : HttpMessageHandler
        //{
        //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
