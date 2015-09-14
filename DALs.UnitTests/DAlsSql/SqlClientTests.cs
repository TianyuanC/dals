namespace DALs.UnitTests.DAlsSql
{
    using ClientRepository.Model;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using DALs.Sql;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class SqlClientTests
    {
        private const string FakeConnection = "server=happy.net; initial catalog=a;uid=a; pwd=pwd; Connect Timeout=90";

        [Test]
        public void Constructor()
        {
            new SqlClient();
        }

        [Test]
        public async Task Command()
        {
            //arrange
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var init = Substitute.For<ISqlInitializer>();
            connection.Open();
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SqlClient(init);

            //act
            var result = await client.CommandAsync(new SqlConfiguration(FakeConnection, "testSproc"));

            //assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task CommandThrow()
        {
            //arrange
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var init = Substitute.For<ISqlInitializer>();
            connection.Open();
            command.ExecuteNonQuery().Throws(call => { throw new Exception();});
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SqlClient(init);

            //act
            var result = await client.CommandAsync(new SqlConfiguration(FakeConnection, "testSproc"));

            //assert
            Assert.AreEqual(-1, result);
        }

        [Test]
        public async Task QueryScalar()
        {
            //arrange
            IEnumerable<Ad> ads = new List<Ad> { new Ad { AdID = Int32.MaxValue } };
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var init = Substitute.For<ISqlInitializer>();
            connection.Open();
            command.ExecuteScalar().Returns(ads);
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SqlClient(init);

            //act
            var result = await client.QueryAsync<IEnumerable<Ad>>(
                new SqlConfiguration(FakeConnection, "testSproc", SprocMode.ExecuteScalar));

            //assert
            Assert.AreEqual(1, result.Count(x => x.AdID == Int32.MaxValue));
        }

        [Test]
        public async Task QueryReader()
        {
            //arrange
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var init = Substitute.For<ISqlInitializer>();
            connection.Open();
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SqlClient(init);

            //act
            var result = await client.QueryAsync<IEnumerable<Ad>>(
                new SqlConfiguration(FakeConnection, "testSproc", SprocMode.ExecuteReader),
                reader => new List<Ad> { new Ad { AdID = Int32.MinValue } });

            //assert
            Assert.AreEqual(1, result.Count(x => x.AdID == Int32.MinValue));
        }

        [Test]
        public async Task QueryThrow()
        {
            //arrange
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var init = Substitute.For<ISqlInitializer>();
            connection.Open();
            command.ExecuteScalar().Throws<Exception>();
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SqlClient(init);

            //act
            var result = await client.QueryAsync<IEnumerable<Ad>>(
                new SqlConfiguration(FakeConnection, "testSproc", SprocMode.ExecuteScalar));

            //assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task CommandMultiple()
        {
            //arrange
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var transaction = Substitute.For<IDbTransaction>();
            var init = Substitute.For<ISqlInitializer>();
            connection.Open();
            connection.BeginTransaction(Arg.Any<IsolationLevel>()).Returns(transaction);
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SqlClient(init);

            //act
            var result = await client.CommandMultipleAsync(new SqlConfiguration(FakeConnection, new List<string> { "testSproc1", "testSproc2" }));

            //assert
            Assert.AreEqual(0, result);
            command.Received(2).ExecuteNonQuery();
            transaction.Received(1).Commit();
        }

        [Test]
        public async Task CommandMultipleThrow()
        {
            //arrange
            var connection = Substitute.For<IDbConnection>();
            var command = Substitute.For<IDbCommand>();
            var transaction = Substitute.For<IDbTransaction>();
            var init = Substitute.For<ISqlInitializer>();
            connection.Open();
            connection.BeginTransaction(Arg.Any<IsolationLevel>()).Returns(transaction);
            command.ExecuteNonQuery().Returns(x => 0, x => { throw new Exception(); });
            init.DbConnection(Arg.Any<string>()).Returns(connection);
            init.DbCommand(Arg.Any<string>()).Returns(command);
            var client = new SqlClient(init);

            //act
            var result = await client.CommandMultipleAsync(new SqlConfiguration(FakeConnection, new List<string> { "testSproc1", "testSproc2" }));

            //assert
            Assert.AreEqual(-1, result);
            command.Received(2).ExecuteNonQuery();
            transaction.Received(0).Commit();
        }
    }
}
