namespace DALs.UnitTests.DAlsSql.Extensions
{
    using DALs.Sql.Extensions;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    [TestFixture]
    public class IDbCommandExtensionsTests
    {
        [Test]
        public async Task Reader()
        {
            var command = Substitute.For<IDbCommand>();
            await command.ExecuteReaderAsync();
            command.Received(1).ExecuteReader();
        }

        [Test]
        public void ReaderAsync()
        {
            IDbCommand command = new SqlCommand();
            Assert.Throws<InvalidOperationException>(async ()=>await command.ExecuteReaderAsync());
        }

        [Test]
        public async Task NonQuery()
        {
            var command = Substitute.For<IDbCommand>();
            await command.ExecuteNonQueryAsync();
            command.Received(1).ExecuteNonQuery();
        }

        [Test]
        public void NonQueryAsync()
        {
            IDbCommand command = new SqlCommand();
            Assert.Throws<InvalidOperationException>(async () => await command.ExecuteNonQueryAsync());
        }

        [Test]
        public async Task Scalar()
        {
            var command = Substitute.For<IDbCommand>();
            await command.ExecuteScalarAsync();
            command.Received(1).ExecuteScalar();
        }

        [Test]
        public void ScalarAsync()
        {
            IDbCommand command = new SqlCommand();
            Assert.Throws<InvalidOperationException>(async () => await command.ExecuteScalarAsync());
        }
    }
}
