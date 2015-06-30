namespace DALs.UnitTests.DAlsSql
{
    using DALs.Sql;
    using NUnit.Framework;

    [TestFixture]
    public class InitSqlHelperTests
    {
        private const string FakeConnection = "server=happy.net; initial catalog=a;uid=a; pwd=pwd; Connect Timeout=90";

        [Test]
        public void DbConnection()
        {
            var init = new SqlInitializer();
            var connection = init.DbConnection(FakeConnection);
            Assert.IsTrue(connection != null);
        }

        [Test]
        public void DbCommand()
        {
            var init = new SqlInitializer();
            var command = init.DbCommand("sproc");
            Assert.IsTrue(command != null);
        }
    }
}
