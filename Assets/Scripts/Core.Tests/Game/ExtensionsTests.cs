using NUnit.Framework;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class ExtensionsTests
    {

        [Test]
        public void Given_Any_TilePosition_When_It_Is_Converted_To_A_Vector3Int_Then_Its_Coordinates_Are_Copied_Correctly()
        {
            // Given

            var tilePosition = new TilePosition(-1, 2);

            // When

            var vector = tilePosition.ToVector3Int();

            // Then

            Assert.That(vector.x, Is.EqualTo(tilePosition.X), "X coordinate not copied correctly!");
            Assert.That(vector.y, Is.EqualTo(tilePosition.Y), "Y coordinate not copied correctly!");
            Assert.That(vector.z, Is.Zero, "Z coordinate should be 0!");
        }

    }

}