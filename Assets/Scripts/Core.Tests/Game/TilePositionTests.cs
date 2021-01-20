using System.Collections.Immutable;
using System.Linq;
using NUnit.Framework;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class TilePositionTests
    {
        
        [Test]
        public void Distance_Between_Positions_Is_Calculated_Correctly()
        {
            // Given

            var p1 = new TilePosition(0, 0);
            var p2 = new TilePosition(2, 0);

            // When

            var distance = DistanceTo(p1, p2);

            // When

            Assert.That(distance, Is.EqualTo(2), "Incorrect distance calculated!");
        }
        
        [Test]
        public void Given_Any_TilePosition_When_It_Is_Converted_To_A_Vector3Int_Then_Its_Coordinates_Are_Copied_Correctly()
        {
            // Given

            var tilePosition = new TilePosition(-1, 2);

            // When

            var vector = tilePosition.Map(ToVector3Int);

            // Then

            Assert.That(vector.x, Is.EqualTo(tilePosition.X), "X coordinate not copied correctly!");
            Assert.That(vector.y, Is.EqualTo(tilePosition.Y), "Y coordinate not copied correctly!");
            Assert.That(vector.z, Is.Zero, "Z coordinate should be 0!");
        }

    }

}