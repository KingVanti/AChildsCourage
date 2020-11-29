using AChildsCourage.Game.Persistance;
using NUnit.Framework;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class NightManagerTests
    {

        [Test]
        public void When_Preparing_Then_The_Current_NightData_Is_Loaded()
        {
            // Given

            var nightData = new NightData();
            RunDataLoader runDataLoader = () => new RunData(nightData);

            var calledTimes = 0;
            NightLoader nightLoader = _ => calledTimes++;

            var nightManager = new NightManager(runDataLoader, nightLoader);

            // When

            nightManager.PrepareNight();

            // Then

            Assert.That(calledTimes, Is.EqualTo(1), "No or incorrect night-data was loaded!");
        }

    }

}