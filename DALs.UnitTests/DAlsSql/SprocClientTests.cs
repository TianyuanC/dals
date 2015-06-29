namespace DALs.UnitTests.DAlsSql
{
    using ClientRepository.Model;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using DALs.Sql;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class SprocClientTests
    {
        private const string FakeConnection = "server=happy.net; initial catalog=a;uid=a; pwd=pwd; Connect Timeout=90";

        [Test]
        public void Constructor()
        {
            new SprocClient();
        }

        [Test]
        public async Task Command()
        {
            //arrange
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var init = Substitute.For<IInitSqlHelper>();
            connection.Open();
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SprocClient(init);

            //act
            var result = await client.CommandAsync(new SqlSprocConfiguration(FakeConnection, "testSproc"));
            
            //assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task QueryScalar()
        {
            //arrange
            IEnumerable<Ad> ads = new List<Ad>();
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var init = Substitute.For<IInitSqlHelper>();
            connection.Open();
            command.ExecuteScalar().Returns(ads);
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SprocClient(init);

            //act
            var result = await client.QueryAsync<IEnumerable<Ad>>(new SqlSprocConfiguration(FakeConnection, "testSproc", SprocMode.ExecuteScalar));

            //assert
            Assert.AreEqual(0, result.Count());
        }
    }
}
