using NUnit.Framework;
using static AChildsCourage.Game.TileOffset;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class TileOffsetTests
    {

        [Test]
        public void Given_A_TilePosition_When_An_Offset_Is_Added_Then_The_Coordinates_Are_Added()
        {
            // Given

            var position = new TilePosition(0, 0);
            var offset = new TileOffset(1, -1);

            // When

            var actual = offset.Map(ApplyTo, position);

            // Then

            Assert.That(actual.X, Is.EqualTo(position.X + offset.X), "X coordinate incorrectly added!");
            Assert.That(actual.Y, Is.EqualTo(position.Y + offset.Y), "Y coordinate incorrectly added!");
        }

    }

}