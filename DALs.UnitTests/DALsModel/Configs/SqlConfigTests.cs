using System.Collections.Generic;

namespace DALs.UnitTests.DALsModel.Configs
{
    using DALs.Model.Configs;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class SqlConfigTests
    {
        [Test]
        public void ConstructorSingle()
        {
            new SqlConfiguration("connection", "sproc");
        }

        [Test]
        public void ConstrutorSingleNullConnection()
        {
            var ex = Assert.Throws<ArgumentNullException>(()=>new SqlConfiguration("", ""));
            Assert.AreEqual("connectionString", ex.ParamName);
        }

        [Test]
        public void ConstrutorSingleNullSproc()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SqlConfiguration("connection", ""));
            Assert.AreEqual("sprocName", ex.ParamName);
        }

        [Test]
        public void ConstructorMultiple()
        {
            new SqlConfiguration("connection", new List<string>{"sproc"});
        }

        [Test]
        public void ConstrutorMultipleNullConnection()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SqlConfiguration("", new List<string> { "sproc" }));
            Assert.AreEqual("connectionString", ex.ParamName);
        }

        [Test]
        public void ConstrutorMultipleNullSproc()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new SqlConfiguration("connection", new List<string>()));
            Assert.AreEqual("sprocNames", ex.ParamName);
        }
    }
}
