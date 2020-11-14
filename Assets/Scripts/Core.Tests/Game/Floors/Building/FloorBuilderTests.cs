using AChildsCourage.Game.Floors.Persistance;
using Moq;
using NUnit.Framework;
using PADEAH.Tabs;
using System.Linq;

namespace AChildsCourage.Game.Floors.Building
{

    [TestFixture]
    public class FloorBuilderTests
    {

        #region Tests

        [Test]
        public void When_A_Floor_Is_Built_Then_An_Event_With_The_Result_Is_Raised()
        {
            // Given

            var roomRepository = GetRoomRepositoryWith();
            var floorBuilder = new FloorBuilder(roomRepository);

            // When

            var raisedArgs =
                ListenFor
                .First<FloorBuiltEventArgs>()
                .From(floorBuilder)
                .During(() => floorBuilder.Build(FloorPlan.Empty));

            // Then

            Assert.That(raisedArgs, Is.Not.Null, "No event was raised!");
            Assert.That(raisedArgs.Floor, Is.Not.Null, "The floor in the event was null!");
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
            var roomRepository = GetRoomRepositoryWith(roomsInChunks);
            var floorBuilder = new FloorBuilder(roomRepository);

            // When

            var floor = floorBuilder.BuildFrom(new RoomsInChunks(roomsInChunks));

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
            var roomRepository = GetRoomRepositoryWith(roomsInChunks);
            var floorBuilder = new FloorBuilder(roomRepository);

            // When

            var floor = floorBuilder.BuildFrom(new RoomsInChunks(roomsInChunks));

            // Then

            Assert.That(floor.WallCount, Is.Not.Zero, "No walls were genereated!");
        }

        #endregion

        #region Helpers

        private IRoomRepository GetRoomRepositoryWith(params RoomInChunk[] roomsInChunks)
        {
            var mockRoomRepository = new Mock<IRoomRepository>();

            mockRoomRepository.Setup(r => r.LoadRoomsFor(It.IsAny<FloorPlan>())).Returns(new RoomsInChunks(roomsInChunks));

            return mockRoomRepository.Object;
        }

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