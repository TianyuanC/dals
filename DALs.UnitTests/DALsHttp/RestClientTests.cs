namespace DALs.UnitTests.DALsHttp
{
    using DALs.Http;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using ClientRepository.Model;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using NSubstitute;

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

        [Test]
        public async Task GetAsync()
        {
            var init = Substitute.For<IHttpInitializer>();
            var messager = new FakeMessageHandler(new HttpResponseMessage());
            init.HttpClient.Returns(new HttpClient(messager));
            var rest = new RestClient(init);
            var config = new HttpConfiguration(new Uri("http://happy.ca/"), "route", HttpRequest.Get);
            
            var ads = await rest.GetAsync(config, response => new List<Ad>{new Ad{Id = long.MaxValue}});

            Assert.AreEqual(1, ads.Count(x=>x.Id==long.MaxValue));
            Assert.AreEqual("http://happy.ca/route", messager.RequestMessage.RequestUri.AbsoluteUri);
            Assert.AreEqual(HttpMethod.Get, messager.RequestMessage.Method);
        }

        [Test]
        public async Task SetSyncPut()
        {
            var init = Substitute.For<IHttpInitializer>();
            var messager = new FakeMessageHandler(new HttpResponseMessage());
            init.HttpClient.Returns(new HttpClient(messager));
            var rest = new RestClient(init);
            var config = new HttpConfiguration(
                new Uri("http://happy.ca/"), "route", HttpRequest.Put) {Data = "data"};

            var ads = await rest.SetAsync(config, response => 
                new List<Ad> { new Ad { Id = long.MaxValue } });

            Assert.AreEqual(1, ads.Count(x => x.Id == long.MaxValue));
            Assert.AreEqual("http://happy.ca/route", messager.RequestMessage.RequestUri.AbsoluteUri);
            Assert.AreEqual(HttpMethod.Put, messager.RequestMessage.Method);
        }

        [Test]
        public async Task SetSyncPost()
        {
            var init = Substitute.For<IHttpInitializer>();
            var messager = new FakeMessageHandler(new HttpResponseMessage());
            init.HttpClient.Returns(new HttpClient(messager));
            var rest = new RestClient(init);
            var config = new HttpConfiguration(
                new Uri("http://happy.ca/"), "route", HttpRequest.Post) { Data = "data" };

            var ads = await rest.SetAsync(config, response =>
                new List<Ad> { new Ad { Id = long.MaxValue } });

            Assert.AreEqual(1, ads.Count(x => x.Id == long.MaxValue));
            Assert.AreEqual("http://happy.ca/route", messager.RequestMessage.RequestUri.AbsoluteUri);
            Assert.AreEqual(HttpMethod.Post, messager.RequestMessage.Method);
        }

        private class FakeMessageHandler : HttpMessageHandler
        {
            private readonly HttpResponseMessage response;

            public FakeMessageHandler(HttpResponseMessage response)
            {
                this.response = response;
            }

            public HttpRequestMessage RequestMessage { get; private set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                RequestMessage = request;
                return Task.FromResult(response);
            }
        }
    }
}
