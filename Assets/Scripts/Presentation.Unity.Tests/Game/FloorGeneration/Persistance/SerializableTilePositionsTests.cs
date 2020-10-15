using NUnit.Framework;
using System.Linq;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [TestFixture]
    public class SerializableTilePositionsTests
    {

        #region Tests

        [Test]
        public void Given_Any_TilePositions_When_They_Are_Made_Serializable_Then_Their_Positions_Are_Correctly_Copied()
        {
            // Given

            var tilePositions = new TilePositions(new[] { new TilePosition(10, -5), new TilePosition(1, 2) });

            // When

            var serializable = SerializableTilePositions.From(tilePositions);

            // Then

            Assert.That(serializable.positions, Is.EqualTo(tilePositions.Positions.Select(SerializableTilePosition.From)), "Positions not correctly copied!");
        }

        [Test]
        public void Given_Any_Serializable_TilePositions_When_TilePositions_Are_Created_Then_Their_Positions_Are_Correctly_Copied()
        {
            // Given

            var serializable = new SerializableTilePositions(new[] { new SerializableTilePosition(10, -5), new SerializableTilePosition(1, 2) });

            // When

            var tilePositions = serializable.ToTilePositions();

            // Then

            Assert.That(tilePositions.Positions, Is.EqualTo(serializable.positions.Select(p => p.ToTilePosition())), "Positions not correctly copied!");
        }

        #endregion

    }

}