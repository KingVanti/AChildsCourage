using AChildsCourage.Game.Floors.Generation;
using AChildsCourage.Game.Persistance;
using Moq;
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

            var mockFloorBuilder = new Mock<IFloorBuilder>();

            var floorPlan = new FloorPlan();
            var mockFloorGenerator = new Mock<IFloorGenerator>();
            mockFloorGenerator.Setup(g => g.GenerateNew(seed)).Returns(floorPlan);

            var nightData = new NightData(seed);
            var nightLoader = new NightLoader(mockFloorGenerator.Object, mockFloorBuilder.Object);

            // When

            nightLoader.Load(nightData);

            // Then

            mockFloorBuilder.Verify(b => b.Build(floorPlan), Times.Once, "No or a different floor was built!");
        }

        #endregion

    }

}