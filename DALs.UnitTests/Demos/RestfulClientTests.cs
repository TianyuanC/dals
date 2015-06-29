namespace DALs.UnitTests.Demos
{
    using ClientRepository;
    using NUnit.Framework;

    [TestFixture]
    public class RestfulClientTests
    {
        [Test]
        public void Constructor()
        {
            new RestfulClient();
        }
    }
}
