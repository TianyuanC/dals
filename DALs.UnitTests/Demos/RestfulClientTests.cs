namespace DALs.UnitTests.Demos
{
    using ClientRepository;
    using ClientRepository.Model;
    using DALs.Model.Configs;
    using DALs.Model.Interfaces;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestFixture]
    public class RestfulClientTests
    {
        [Test]
        public void Constructor()
        {
            new RestfulClient();
        }

        [Test]
        public void ConstructorNullClient()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new RestfulClient(null));
            Assert.AreEqual("restClient", ex.ParamName);
        }

        [Test]
        public async Task GetAsync()
        {
            //arrange
            var rest = Substitute.For<IRestClient>();
            var client = new RestfulClient(rest);
            IEnumerable<Ad> ads = new List<Ad>();
            rest.GetAsync(Arg.Any<HttpClientConfig>(), Arg.Any<Func<HttpResponseMessage, IEnumerable<Ad>>>())
                .Returns(Task.FromResult(ads));
            
            //act
            var ret = await client.GetAsync(new List<long> { 1 });
            
            //assert
            rest.Received(1).GetAsync(
                    Arg.Is<HttpClientConfig>(x => x.Route == "api/ad"),
                    Arg.Any<Func<HttpResponseMessage, IEnumerable<Ad>>>());
        }

        [Test]
        public void LoadAd()
        {
            var ads = RestfulClient.LoadAds(new HttpResponseMessage());
            Assert.AreEqual(0, ads.Count());
        }
    }
}
