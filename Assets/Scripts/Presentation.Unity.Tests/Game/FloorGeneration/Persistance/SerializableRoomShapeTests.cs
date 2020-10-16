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
            var groundPositions = new TilePositions(new[] { new TilePosition(-1, 0) });
            var shape = new RoomShape(wallPositions, groundPositions);

            // When

            var serializable = SerializableRoomShape.From(shape);

            // Then

            var expectedWallPositions = SerializableTilePositions.From(shape.WallPositions);
            var expectedGroundPositions = SerializableTilePositions.From(shape.GroundPositions);

            Assert.That(serializable.wallPositions, Is.EqualTo(expectedWallPositions), "Wall positions not correctly copied!");
            Assert.That(serializable.groundPositions, Is.EqualTo(expectedGroundPositions), "Ground positions not correctly copied!");
        }

        [Test]
        public void Given_Any_Serializable_RoomShape_When_A_RoomShape_Is_Created_Then_Its_Properties_Are_Correctly_Copied()
        {
            // Given

            var wallPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(1, 1), new SerializableTilePosition(-1, 3) });
            var groundPositions = new SerializableTilePositions(new[] { new SerializableTilePosition(-1, 0) });
            var serializable = new SerializableRoomShape(wallPositions, groundPositions);

            // When

            var shape = serializable.ToRoomShape();

            // Then

            var expectedWallPositions = serializable.wallPositions.ToTilePositions();
            var expectedGroundPositions = serializable.groundPositions.ToTilePositions();

            Assert.That(shape.WallPositions, Is.EqualTo(expectedWallPositions), "Wall positions not correctly copied!");
            Assert.That(shape.GroundPositions, Is.EqualTo(expectedGroundPositions), "Ground positions not correctly copied!");
        }

        #endregion

    }

}