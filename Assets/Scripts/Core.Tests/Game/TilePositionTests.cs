using NUnit.Framework;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class TilePositionTests
    {

        #region Tests

        [Test]
        public void Given_Any_Coordinates_When_A_TilePosition_Is_Created_From_Them_Then_It_Has_These_Coordinates()
        {
            // Given

            var x = 5;
            var y = -10;

            // When

            var position = new TilePosition(x, y);

            // Then

            Assert.That(position.X, Is.EqualTo(x), "Incorrect X coordinate!");
            Assert.That(position.Y, Is.EqualTo(y), "Incorrect Y coordinate!");
        }

        [Test]
        public void Given_A_TilePosition_When_An_Offset_Is_Added_Then_The_Cooridantes_Are_Added()
        {
            // Given

            var position = new TilePosition(0, 0);
            var offset = new TileOffset(1, -1);

            // When

            var actual = position + offset;

            // Then

            Assert.That(actual.X, Is.EqualTo(position.X + offset.X), "X coordinate incorrectly added!");
            Assert.That(actual.Y, Is.EqualTo(position.Y + offset.Y), "Y coordinate incorrectly added!");
        }

        #endregion

    }

}