namespace DALs.UnitTests.DAlsSql.Extensions
{
    using DALs.Sql.Extensions;
    using NSubstitute;
    using NUnit.Framework;
    using System.Data;

    [TestFixture]
    public class IDbConnectionExtensionsTests
    {
        [Test]
        public void ForceClose()
        {
            var connection = Substitute.For<IDbConnection>();
            connection.State.Returns(ConnectionState.Open);
            connection.ForceClose();
            connection.Received(1).Close();
            connection.Received(1).Dispose();
        }

        [Test]
        public void ForceCloseAlreadyClosed()
        {
            var connection = Substitute.For<IDbConnection>();
            connection.State.Returns(ConnectionState.Closed);
            connection.ForceClose();
            connection.Received(0).Close();
            connection.Received(0).Dispose();
        }
    }
}
