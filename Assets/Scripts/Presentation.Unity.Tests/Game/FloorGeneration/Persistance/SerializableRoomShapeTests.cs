using NUnit.Framework;
using System.Linq;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [TestFixture]
    public class SerializableRoomShapeTests
    {

        #region Tests

        [Test]
        public void Given_Any_RoomShape_When_It_Is_Made_Serializable_Then_Its_Properties_Are_Correctly_Copied()
        {
            // Given

            var wallPositions = new[] { new TilePosition(1, 1), new TilePosition(-1, 3) };
            var floorPositions = new[] { new TilePosition(-1, 0) };
            var shape = new RoomShape(wallPositions, floorPositions);

            // When

            var serializable = SerializableRoomShape.From(shape);

            // Then

            Assert.That(serializable.wallPositions, Is.EqualTo(shape.WallPositions.Select(SerializableTilePosition.From)), "Wall positions not correctly copied!");
            Assert.That(serializable.floorPositions, Is.EqualTo(shape.FloorPositions.Select(SerializableTilePosition.From)), "Floor positions not correctly copied!");
        }

        [Test]
        public void Given_Any_Serializable_RoomShape_When_A_RoomShape_Is_Created_Then_Its_Properties_Are_Correctly_Copied()
        {
            // Given

            var wallPositions = new[] { new SerializableTilePosition(1, 1), new SerializableTilePosition(-1, 3) };
            var floorPositions = new[] { new SerializableTilePosition(-1, 0) };
            var serializable = new SerializableRoomShape(wallPositions, floorPositions);

            // When

            var shape = serializable.ToRoomShape();

            // Then

            Assert.That(shape.WallPositions, Is.EqualTo(serializable.wallPositions.Select(p => p.ToTilePosition())), "Wall positions not correctly copied!");
            Assert.That(shape.FloorPositions, Is.EqualTo(serializable.floorPositions.Select(p => p.ToTilePosition())), "Floor positions not correctly copied!");
        }

        #endregion

    }

}