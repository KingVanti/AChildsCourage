using NUnit.Framework;

namespace AChildsCourage.Game.Floors.Persistance
{

    [TestFixture]
    public class SerializableRoomShapeTests
    {

        #region Tests

        [Test]
        public void Given_Any_RoomShape_When_It_Is_Made_Serializable_Then_Its_Properties_Are_Correctly_Copied()
        {
            // Given

            var wallPositions = new TilePositions(new[] { new TilePosition(1, 1), new TilePosition(-1, 3) });
            var shape = new RoomShape(wallPositions);

            // When

            var serializable = SerializableRoomShape.From(shape);

            // Then

            var expectedWallPositions = SerializableTilePositions.From(shape.WallPositions);

            Assert.That(serializable.wallPositions, Is.EqualTo(expectedWallPositions), "Wall positions not correctly copied!");
        }

        [Test]
        public void Given_Any_Serializable_RoomShape_When_A_RoomShape_Is_Created_Then_Its_Properties_Are_Correctly_Copied()
        {
            // Given

            var wallPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(1, 1), new SerializableTilePosition(-1, 3) });
            var serializable = new SerializableRoomShape(wallPositions);

            // When

            var shape = serializable.ToRoomShape();

            // Then

            var expectedWallPositions = serializable.wallPositions.ToTilePositions();

            Assert.That(shape.WallPositions, Is.EqualTo(expectedWallPositions), "Wall positions not correctly copied!");
        }

        #endregion

    }

}