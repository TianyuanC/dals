namespace DALs.UnitTests.DALsHttp.Validation
{
    using DALs.Http.Validation;
    using DALs.Model.Enums;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class HttpRequestValidationTests
    {
        [Test]
        [TestCase(HttpRequest.Put)]
        [TestCase(HttpRequest.Post)]
        [TestCase(HttpRequest.Delete)]
        public void GetThrow(HttpRequest request)
        {
            var ex = Assert.Throws<ArgumentException>(()=>request.ValidateGet());
            Assert.AreEqual("RequestMethod", ex.Message);
        }

        [Test]
        [TestCase(HttpRequest.Get)]
        [TestCase(HttpRequest.Delete)]
        public void SetThrow(HttpRequest request)
        {
            var ex = Assert.Throws<ArgumentException>(() => request.ValidateSet());
            Assert.AreEqual("RequestMethod", ex.Message);
        }

        [Test]
        public void Get()
        {
            HttpRequest.Get.ValidateGet();
        }

        [Test]
        [TestCase(HttpRequest.Put)]
        [TestCase(HttpRequest.Post)]
        public void Set(HttpRequest request)
        {
            request.ValidateSet();
        }
    }
}
