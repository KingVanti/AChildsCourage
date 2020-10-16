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
            var floorPositions = new TilePositions(new[] { new TilePosition(-1, 0) });
            var shape = new RoomShape(wallPositions, floorPositions);

            // When

            var serializable = SerializableRoomShape.From(shape);

            // Then

            var expectedWallPositions = SerializableTilePositions.From(shape.WallPositions);
            var expectedFloorPositions = SerializableTilePositions.From(shape.FloorPositions);

            Assert.That(serializable.wallPositions, Is.EqualTo(expectedWallPositions), "Wall positions not correctly copied!");
            Assert.That(serializable.floorPositions, Is.EqualTo(expectedFloorPositions), "Floor positions not correctly copied!");
        }

        [Test]
        public void Given_Any_Serializable_RoomShape_When_A_RoomShape_Is_Created_Then_Its_Properties_Are_Correctly_Copied()
        {
            // Given

            var wallPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(1, 1), new SerializableTilePosition(-1, 3) });
            var floorPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(-1, 0) });
            var serializable = new SerializableRoomShape(wallPositions, floorPositions);

            // When

            var shape = serializable.ToRoomShape();

            // Then

            var expectedWallPositions = serializable.wallPositions.ToTilePositions();
            var expectedFloorPositions = serializable.floorPositions.ToTilePositions();

            Assert.That(shape.WallPositions, Is.EqualTo(expectedWallPositions), "Wall positions not correctly copied!");
            Assert.That(shape.FloorPositions, Is.EqualTo(expectedFloorPositions), "Floor positions not correctly copied!");
        }

        #endregion

    }

}