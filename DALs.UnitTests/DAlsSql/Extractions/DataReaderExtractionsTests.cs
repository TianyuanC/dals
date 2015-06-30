namespace DALs.UnitTests.DAlsSql.Extractions
{
    using DALs.Sql.Extractions;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using NUnit.Framework;
    using System;
    using System.Data;

    [TestFixture]
    public class DataReaderExtractionsTests
    {
        [Test]
        public void GetDbNull()
        {
            var reader = Substitute.For<IDataReader>();
            reader.IsDBNull(Arg.Any<int>()).Returns(true);
            var ret = reader.Get(1,1);
            Assert.AreEqual(1,ret);
        }

        [Test]
        public void Get()
        {
            var reader = Substitute.For<IDataReader>();
            reader.IsDBNull(Arg.Any<int>()).Returns(false);
            reader[1].Returns(2);
            var ret = reader.Get(1, 1);
            Assert.AreEqual(2, ret);
        }

        [Test]
        public void GetThrow()
        {
            var reader = Substitute.For<IDataReader>();
            reader.IsDBNull(Arg.Any<int>()).Returns(false);
            reader[1].Throws<InvalidCastException>();
            Assert.Throws<InvalidCastException>(()=> reader.Get(1, 1));
        }
    }
}
