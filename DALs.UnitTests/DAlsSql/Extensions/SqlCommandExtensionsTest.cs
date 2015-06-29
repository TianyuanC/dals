namespace DALs.UnitTests.DAlsSql.Extensions
{
    using DALs.Sql.Extensions;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    [TestFixture]
    public class SqlCommandExtensionsTest
    {
        [Test]
        public void LoadParameters()
        {
            var command = new SqlCommand();
            command.LoadParameters(new List<SqlParameter>{new SqlParameter()});
            Assert.AreEqual(CommandType.StoredProcedure, command.CommandType);
            Assert.AreEqual(1, command.Parameters.Count);
        }
    }
}
