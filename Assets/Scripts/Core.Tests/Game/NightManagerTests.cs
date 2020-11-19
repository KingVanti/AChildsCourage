using AChildsCourage.Game.NightManagement;
using AChildsCourage.Game.NightManagement.Loading;
using AChildsCourage.Game.Persistance;
using Moq;
using NUnit.Framework;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class NightManagerTests
    {

        #region Tests

        [Test]
        public void When_Preparing_Then_The_Current_NightData_Is_Loaded()
        {
            // Given

            var nightData = new NightData();
            var mockRunStorage = new Mock<IRunStorage>();
            mockRunStorage.Setup(s => s.LoadCurrent()).Returns(new RunData(nightData));

            var mockNightLoader = new Mock<INightLoader>();

            var nightManager = new NightManager(mockRunStorage.Object, mockNightLoader.Object);

            // When

            nightManager.PrepareNight();

            // Then

            mockNightLoader.Verify(s => s.Load(nightData), Times.Once, "No or incorrect night-data was loaded!");
        }

        #endregion

    }

}