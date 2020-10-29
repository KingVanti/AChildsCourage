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
        public void Given_Two_TilePositions_When_They_Are_Added_Then_Their_Cooridantes_Are_Added()
        {
            // Given

            var t1 = new TilePosition(0, 0);
            var t2 = new TilePosition(1, -1);

            // When

            var actual = t1 + t2;

            // Then

            Assert.That(actual.X, Is.EqualTo(t1.X + t2.X), "X coordinate incorrectly added!");
            Assert.That(actual.Y, Is.EqualTo(t1.Y + t2.Y), "Y coordinate incorrectly added!");
        }

        #endregion

    }

}