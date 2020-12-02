using AChildsCourage.Game.Persistance;
using NUnit.Framework;
using static AChildsCourage.Game.MNightPreparation;

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
            LoadRunData loadRunData = () => new RunData(nightData);

            var calledTimes = 0;
            PrepareNight prepareNight = _ => calledTimes++;

            var nightManager = new NightManager(loadRunData, prepareNight);

            // When

            nightManager.PrepareNight();

            // Then

            Assert.That(calledTimes, Is.EqualTo(1), "No or incorrect night-data was loaded!");
        }

    }

}