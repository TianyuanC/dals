namespace DALs.UnitTests.DALsModel.Configs
{
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class SqlSprocConfigsTests
    {
        [Test]
        public void Constructor()
        {
            new SqlSprocConfiguration("connection", "sproc");
        }

        [Test]
        public void ConstructorSecond()
        {
            new SqlSprocConfiguration("connection", "sproc", SprocMode.ExecuteReader);
        }
    }
}
