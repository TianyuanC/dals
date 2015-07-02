namespace DALs.UnitTests.DALsModel
{
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class HttpClinetConfigTests
    {
        [Test]
        public void Constructor()
        {
            new HttpConfiguration(new Uri("https://happy.ca/"), "route", HttpRequest.Get);
        }
    }
}
