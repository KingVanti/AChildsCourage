using NUnit.Framework;
using static AChildsCourage.Game.TileOffset;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class TileOffsetTests
    {

        [Test]
        public void Offsetting_A_TilePosition_Changes_Its_Coordinates_Correctly([Random(-100, 100, 2)] int x, [Random(-100, 100, 2)] int y, [Random(-100, 100, 2)] int oX, [Random(-100, 100, 2)] int oY)
        {
            // Given

            var position = new TilePosition(x, y);
            var offset = new TileOffset(oX, oY);

            // When

            var actual = offset.Map(ApplyTo, position);

            // Then

            Assert.That(actual.X, Is.EqualTo(position.X + offset.X), "X coordinate incorrectly added!");
            Assert.That(actual.Y, Is.EqualTo(position.Y + offset.Y), "Y coordinate incorrectly added!");
        }

    }

}