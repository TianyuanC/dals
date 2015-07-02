namespace DALs.UnitTests.Demos
{
    using ClientRepository;
    using ClientRepository.Model;
    using DALs.Model.Configs;
    using DALs.Model.Enums;
    using DALs.Model.Interfaces;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class DbClientTests
    {
        [Test]
        public void Constructors()
        {
            new DbClient();
        }

        [Test]
        public void ConstructorNullSprocs()
        {
            var ex = Assert.Throws<ArgumentNullException>(()=>new DbClient(null));
            Assert.AreEqual("sqlClient", ex.ParamName);
        }

        [Test]
        public async Task Get()
        {
            //arrange
            var sprocs = Substitute.For<ISqlClient>();
            var dbClient = new DbClient(sprocs);
            IEnumerable<Ad> ads = new List<Ad> { new Ad { Id = 1 } };
            sprocs.QueryAsync(Arg.Any<SqlConfiguration>(), Arg.Any<Func<IDataReader, IEnumerable<Ad>>>()).Returns(Task.FromResult(ads));

            //act
            var ret = await dbClient.GetAsync();

            //assert
            Assert.AreEqual(1, ret.Count(x=>x.Id==1));
            sprocs.Received(1).QueryAsync(
                    Arg.Is<SqlConfiguration>(
                        x => x.Mode == SprocMode.ExecuteReader && x.StoredProcedures.Contains(Settings.GetAd)),
                    Arg.Any<Func<IDataReader, IEnumerable<Ad>>>());
        }

        [Test]
        public void AdsLoader()
        {
            //arrange
            var reader = Substitute.For<IDataReader>();
            
            reader.GetOrdinal(Arg.Any<string>()).Returns(x => 0, x => 1, x => 2); //indexes
            reader.Read().Returns(x => true, x => true, x => false); //load two rounds
            reader.IsDBNull(Arg.Any<int>()).Returns(false);
            reader[0].Returns(x => (long)1, x => (long)2);
            reader[1].Returns(DateTime.Now);
            reader[2].Returns("tests");

            //act
            var ads = DbClient.AdsLoader(reader);

            //assert
            Assert.AreEqual(2, ads.Count());
            Assert.AreEqual(2, ads.Count(x=>x.Content.Equals("tests")));
            Assert.AreEqual(new List<long>{1,2}, ads.Select(x=>x.Id));
        }
    }
}
