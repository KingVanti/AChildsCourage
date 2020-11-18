using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.Floors.FloorTilesBuilding;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class CouragePlacingTests
    {

        #region Tests

        [Test]
        public void Courage_Orb_Weight_Is_Proportional_To_Distance_From_Origin()
        {
            // Given

            var position1 = new TilePosition(1, 0);
            var position2 = new TilePosition(2, 0);

            // When

            var weight1 = CalculateCourageOrbWeight(position1, Enumerable.Empty<TilePosition>());
            var weight2 = CalculateCourageOrbWeight(position2, Enumerable.Empty<TilePosition>());

            // Then

            Assert.That(weight1, Is.LessThan(weight2), "Weight 1 should be smaller than weight 2!");
        }

        [Test]
        public void Courage_Orb_Weight_Is_Proportional_To_Minimum_Distance_To_Other_Orbs()
        {
            // Given

            var position1 = new TilePosition(1, 0);
            var position2 = new TilePosition(1, 0);

            // When

            var weight1 = CalculateCourageOrbWeight(position1, new[] { new TilePosition(2, 0) });
            var weight2 = CalculateCourageOrbWeight(position2, new[] { new TilePosition(3, 0) });

            // Then

            Assert.That(weight1, Is.LessThan(weight2), "Weight 1 should be smaller than weight 2!");
        }

        #endregion

    }

}