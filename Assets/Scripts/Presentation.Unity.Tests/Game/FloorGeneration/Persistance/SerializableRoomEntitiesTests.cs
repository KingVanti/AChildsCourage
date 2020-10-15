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
            var smallCouragePositions = new TilePositions(new[] { new TilePosition(0, 1) });
            var bigCouragePositions = new TilePositions(new[] { new TilePosition(1, 0) });

            var entities = new RoomEntities(itemPositions, smallCouragePositions, bigCouragePositions);

            // When

            var serializable = SerializableRoomEntities.From(entities);

            // Then

            var expectedItemPositions = SerializableTilePositions.From(itemPositions);
            var expectedSmallCouragePositions = SerializableTilePositions.From(smallCouragePositions);
            var expectedBigCouragePositions = SerializableTilePositions.From(bigCouragePositions);

            Assert.That(serializable.itemPositions, Is.EqualTo(expectedItemPositions), "Item positions not correctly copied!");
            Assert.That(serializable.smallCouragePositions, Is.EqualTo(expectedSmallCouragePositions), "Small courage positions not correctly copied!");
            Assert.That(serializable.bigCouragePositions, Is.EqualTo(expectedBigCouragePositions), "Big courage not correctly copied!");
        }

        [Test]
        public void Given_Any_Serialized_RoomEntities_When_RoomEntities_Are_Created_Then_Their_Properties_Are_Correctly_Copied()
        {
            // Given

            var itemPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(1, 1), new SerializableTilePosition(-1, 3) });
            var smallCouragePositions = new SerializableTilePositions(new[] { new SerializableTilePosition(0, 1) });
            var bigCouragePositions = new SerializableTilePositions(new[] { new SerializableTilePosition(1, 0) });

            var serialized = new SerializableRoomEntities(itemPositions, smallCouragePositions, bigCouragePositions);

            // When

            var entities = serialized.ToRoomEntities();

            // Then

            var expectedItemPositions = itemPositions.ToTilePositions();
            var expectedSmallCouragePositions = smallCouragePositions.ToTilePositions();
            var expectedBigCouragePositions = bigCouragePositions.ToTilePositions();

            Assert.That(entities.ItemPositions, Is.EqualTo(expectedItemPositions), "Item positions not correctly copied!");
            Assert.That(entities.SmallCouragePositions, Is.EqualTo(expectedSmallCouragePositions), "Small courage positions not correctly copied!");
            Assert.That(entities.BigCouragePositions, Is.EqualTo(expectedBigCouragePositions), "Big courage positions not correctly copied!");
        }

        #endregion

    }

}