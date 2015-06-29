namespace DALs.UnitTests.DAlsSql
{
    using ClientRepository.Model;
    using DALs.Model.Configs;
    using DALs.Sql;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class SprocClientTests
    {
        private const string FakeConnection = "server=happy.net; initial catalog=a;uid=a; pwd=pwd; Connect Timeout=90";

        [Test]
        public async Task Command()
        {
            var client = new SprocClient();
            var result = await client.CommandAsync(new SqlSprocConfiguration(FakeConnection, "testSproc"));
            Assert.AreEqual(-1, result);
        }

        [Test]
        public async Task Query()
        {
            var client = new SprocClient();
            var result = await client.QueryAsync<IEnumerable<Ad>>(new SqlSprocConfiguration(FakeConnection, "testSproc"));
        }
    }
}
