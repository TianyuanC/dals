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
        public void GetIndexDbNull()
        {
            var reader = Substitute.For<IDataReader>();
            reader.IsDBNull(Arg.Any<int>()).Returns(true);
            var ret = reader.Get(1,1);
            Assert.AreEqual(1,ret);
        }

        [Test]
        public void GetIndex()
        {
            var reader = Substitute.For<IDataReader>();
            reader.IsDBNull(Arg.Any<int>()).Returns(false);
            reader[1].Returns(2);
            var ret = reader.Get(1, 1);
            Assert.AreEqual(2, ret);
        }

        [Test]
        public void GetIndexThrow()
        {
            var reader = Substitute.For<IDataReader>();
            reader.IsDBNull(Arg.Any<int>()).Returns(false);
            reader[1].Returns("test");
            Assert.Throws<InvalidCastException>(()=> reader.Get(1, 1));
        }

        [Test]
        public void GetColumnName()
        {
            var reader = Substitute.For<IDataReader>();
            reader.GetOrdinal(Arg.Any<string>()).Returns(1);
            reader.IsDBNull(1).Returns(false);
            reader[1].Returns(2);
            var ret = reader.Get("test", 1);
            Assert.AreEqual(2, ret);
        }

        [Test]
        public void GetColumnNameThrow()
        {
            var reader = Substitute.For<IDataReader>();
            reader.GetOrdinal(Arg.Any<string>()).Throws(new IndexOutOfRangeException());
            Assert.Throws<IndexOutOfRangeException>(() => reader.Get("test", 1));
        }
    }
}
