using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Persistance;
using NUnit.Framework;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class NightLoaderTests
    {

        #region Tests

        [Test]
        public void When_A_Night_Is_Loaded_Then_The_Floor_Generated_From_The_Seed_Is_Built()
        {
            // Given

            var seed = 0;

            var expected = new FloorPlan();
            FloorGenerator floorGenerator = s => expected;

            var actual = (FloorPlan)null;
            FloorTilesBuilder builder = p => { actual = p; return null; };

            var nightData = new NightData(seed);
            var nightLoader = new NightLoader(floorGenerator, builder);

            // When

            nightLoader.Load(nightData);

            // Then

            Assert.That(actual, Is.Not.Null, "No floorplan was built!");
            Assert.That(actual, Is.EqualTo(expected), "A different floorplan was built!");
        }

        #endregion

    }

}