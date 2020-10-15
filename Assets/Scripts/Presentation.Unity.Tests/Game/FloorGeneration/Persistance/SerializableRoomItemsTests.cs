using NUnit.Framework;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [TestFixture]
    public class SerializableRoomItemsTests
    {

        #region Tests

        [Test]
        public void Given_Any_RoomItems_When_They_Are_Made_Serializable_Then_Their_Properties_Are_Correctly_Copied()
        {
            // Given

            var potentialSpawnPositions = new TilePositions(new[] { new TilePosition(1, 1), new TilePosition(-1, 3) });
            var items = new RoomItems(potentialSpawnPositions);

            // When

            var serializable = SerializableRoomItems.From(items);

            // Then

            var expectedPotentialSpawnPositions = SerializableTilePositions.From(potentialSpawnPositions);
            Assert.That(serializable.potentialSpawnPositions, Is.EqualTo(expectedPotentialSpawnPositions), "Potential spawn positions not correctly copied!");
        }

        [Test]
        public void Given_Any_Serialized_RoomItems_When_RoomItems_Are_Created_Then_Their_Properties_Are_Correctly_Copied()
        {
            // Given

            var potentialSpawnPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(1, 1), new SerializableTilePosition(-1, 3) });
            var serialized = new SerializableRoomItems(potentialSpawnPositions);

            // When

            var items = serialized.ToRoomItems();

            // Then

            var expectedPotentialSpawnPositions = potentialSpawnPositions.ToTilePositions();
            Assert.That(items.PotentialSpawnPositions, Is.EqualTo(expectedPotentialSpawnPositions), "Potential spawn positions not correctly copied!");
        }

        #endregion

    }

}