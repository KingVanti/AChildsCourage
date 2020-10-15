using NUnit.Framework;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [TestFixture]
    public class SerializableRoomEntitiesTests
    {

        #region Tests

        [Test]
        public void Given_Any_RoomEntities_When_They_Are_Made_Serializable_Then_Their_Properties_Are_Correctly_Copied()
        {
            // Given

            var itemPositions = new TilePositions(new[] { new TilePosition(1, 1), new TilePosition(-1, 3) });
            var entities = new RoomEntities(itemPositions);

            // When

            var serializable = SerializableRoomEntities.From(entities);

            // Then

            var expectedItemPositions = SerializableTilePositions.From(itemPositions);
            Assert.That(serializable.itemPositions, Is.EqualTo(expectedItemPositions), "Item positions not correctly copied!");
        }

        [Test]
        public void Given_Any_Serialized_RoomEntities_When_RoomEntities_Are_Created_Then_Their_Properties_Are_Correctly_Copied()
        {
            // Given

            var itemPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(1, 1), new SerializableTilePosition(-1, 3) });
            var serialized = new SerializableRoomEntities(itemPositions);

            // When

            var entities = serialized.ToRoomEntities();

            // Then

            var expectedItemPositions = itemPositions.ToTilePositions();
            Assert.That(entities.ItemPositions, Is.EqualTo(expectedItemPositions), "Item positions not correctly copied!");
        }

        #endregion

    }

}