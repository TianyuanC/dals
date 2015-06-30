namespace DALs.UnitTests.DALsHttp.Validation
{
    using System;
    using DALs.Http.Validation;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class HttpConfigValidationTests
    {
        [Test]
        public void BaseUri()
        {
            var config = new HttpConfiguration(new Uri("http://happy.ca/"), "", HttpRequest.Get);
            var ex = Assert.Throws<ArgumentException>(()=>config.ValidateBase());
            Assert.AreEqual("Route", ex.Message);
        }

        [Test]
        public void Base()
        {
            var config = new HttpConfiguration(new Uri("http://happy.ca/"), "happy", HttpRequest.Get);
            config.ValidateBase();
        }

        [Test]
        public void Get()
        {
            var config = new HttpConfiguration(new Uri("http://happy.ca/"), "happy", HttpRequest.Get);
            config.ValidateGet();
        }

        [Test]
        public void SetData()
        {
            var config = new HttpConfiguration(new Uri("http://happy.ca/"), "happy", HttpRequest.Put);
            var ex = Assert.Throws<ArgumentException>(()=>config.ValidateSet());
            Assert.AreEqual("Data", ex.Message);
        }

        [Test]
        public void Set()
        {
            var config = new HttpConfiguration(new Uri("http://happy.ca/"), "happy", HttpRequest.Put)
                {Data = "test"};
            config.ValidateSet();
        }
    }
}
