using NUnit.Framework;
using System.Linq;

using static AChildsCourage.Game.Floors.Building.FloorBuildingModule;

namespace AChildsCourage.Game.Floors.Building
{

    [TestFixture]
    public class FloorBuildingModuleTests
    {

        #region Tests


        [Test]
        public void Tile_Offset_Is_Chunk_Position_Times_Chunk_Size()
        {
            // Given

            var chunkPosition = new ChunkPosition(1, -1);

            // When

            var offset = GetTileOffsetFor(chunkPosition);

            // Then

            Assert.That(offset.X, Is.EqualTo(41), "X coordinate incorrect!");
            Assert.That(offset.Y, Is.EqualTo(-41), "Y coordinate incorrect!");
        }

        [Test]
        public void When_A_Floor_Is_Built_Then_All_Ground_Tiles_In_The_FloorPlan_Are_Placed()
        {
            // Given

            var roomsInChunks = new[]
            {
                new RoomInChunk(new ChunkPosition(0, 0), GetRoomWithGroundPositions(new TilePosition(1, 1))),
                new RoomInChunk(new ChunkPosition(1, 0), GetRoomWithGroundPositions(new TilePosition(1, 1)))
            };

            // When

            var floor = BuildFrom(new RoomsInChunks(roomsInChunks));

            // Then

            Assert.That(floor.GroundTileCount, Is.EqualTo(2), "Incorrect number of ground-tiles placed!");
            Assert.That(floor.GroundTilePositions.First(), Is.EqualTo(new TilePosition(1, 1)), "Positions not offset correctly!");
        }

        [Test]
        public void When_A_Floor_Is_Built_Then_Walls_Are_Generated()
        {
            // Given

            var roomsInChunks = new[]
            {
                new RoomInChunk(new ChunkPosition(0, 0), GetRoomWithGroundPositions(new TilePosition(1, 1)))
            };

            // When

            var floor = BuildFrom(new RoomsInChunks(roomsInChunks));

            // Then

            Assert.That(floor.WallCount, Is.Not.Zero, "No walls were genereated!");
        }

        #endregion

        #region Helpers

        private Room GetRoomWithGroundPositions(params TilePosition[] groundPositions)
        {
            var roomTiles = new RoomTiles(
                new Tiles<GroundTile>(groundPositions.Select(p => new GroundTile(p, 1, 0))),
                Tiles<DataTile>.None,
                Tiles<AOIMarker>.None);

            return new Room(
                RoomType.Normal,
                roomTiles,
                ChunkPassages.None);
        }

        #endregion

    }

}