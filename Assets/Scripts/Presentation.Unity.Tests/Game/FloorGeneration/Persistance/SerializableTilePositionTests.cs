using NUnit.Framework;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [TestFixture]
    public class SerializableTilePositionTests
    {

        #region Tests

        [Test]
        public void Given_Any_TilePosition_When_It_Is_Made_Serializable_Then_Its_Coordinates_Are_Correctly_Copied()
        {
            // Given

            var tilePosition = new TilePosition(10, -5);

            // When

            var serializable = SerializableTilePosition.From(tilePosition);

            // Then

            Assert.That(serializable.x, Is.EqualTo(tilePosition.X), "X position not correctly copied!");
            Assert.That(serializable.y, Is.EqualTo(tilePosition.Y), "Y position not correctly copied!");
        }

        [Test]
        public void Given_Any_Serializable_TilePosition_When_A_Tile_Position_Is_Created_Then_Its_Coordinates_Are_Correctly_Copied()
        {
            // Given

            var serializable = new SerializableTilePosition(10, -5);

            // When

            var tilePosition = serializable.ToTilePosition();

            // Then

            Assert.That(tilePosition.X, Is.EqualTo(serializable.x), "X position not correctly copied!");
            Assert.That(tilePosition.Y, Is.EqualTo(serializable.y), "Y position not correctly copied!");
        }

        #endregion

    }

}