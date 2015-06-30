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
            new SprocConfiguration("connection", "sproc");
        }

        [Test]
        public void ConstructorSecond()
        {
            new SprocConfiguration("connection", "sproc", SprocMode.ExecuteReader);
        }
    }
}
