using NUnit.Framework;
using static AChildsCourage.Game.NightLoading.FloorGenerating;

namespace AChildsCourage.Game.NightLoading
{

    [TestFixture]
    public class FloorGeneratingUtilityTests
    {

        [Test]
        public void Distance_From_Origin_Is_Caluclated_Correctly()
        {
            // Given

            var position = new TilePosition(2, 0);

            // When

            var distance = GetDistanceFromOrigin(position);

            // When

            Assert.That(distance, Is.EqualTo(2), "Incorrect distance calculated!");
        }


        [Test]
        public void Distance_Between_Positions_Is_Caluclated_Correctly()
        {
            // Given

            var p1 = new TilePosition(0, 0);
            var p2 = new TilePosition(2, 0);

            // When

            var distance = GetDistanceBetween(p1, p2);

            // When

            Assert.That(distance, Is.EqualTo(2), "Incorrect distance calculated!");
        }

    }

}