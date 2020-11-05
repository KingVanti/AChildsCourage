using AChildsCourage.Game.Floors.Persistance;
using Moq;
using NUnit.Framework;
using PADEAH.Tabs;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class RoomBuilderTests
    {

        #region Tests

        [Test]
        public void When_A_Room_Is_Built_Then_All_Walls_Are_Placed()
        {
            // Given

            var roomData = new RoomData(
                new TilePosition[0],
                new[] { new TilePosition(0, 0), new TilePosition(1, 1) },
                new TilePosition[0],
                new TilePosition[0],
                new TilePosition[0]);
            var mockRoomRepository = new Mock<IRoomRepository>();
            mockRoomRepository.Setup(r => r.Load(0)).Returns(roomData);

            var mockTileBuilder = new Mock<ITileBuilder>();

            var roombuilder = new RoomBuilder(mockTileBuilder.Object, mockRoomRepository.Object);

            // When

            roombuilder.Build(new RoomInChunk(0, new ChunkPosition(1, 1)));

            // Then

            mockTileBuilder.Verify(b => b.PlaceWall(It.IsAny<TilePosition>()), Times.Exactly(2), $"Incorrect number of walls placed!");
        }

        [Test]
        public void When_A_Room_Is_Built_Then_All_Ground_Is_Placed()
        {
            // Given

            var roomData = new RoomData(
                new[] { new TilePosition(0, 0), new TilePosition(1, 1) },
                new TilePosition[0],
                new TilePosition[0],
                new TilePosition[0],
                new TilePosition[0]);
            var mockRoomRepository = new Mock<IRoomRepository>();
            mockRoomRepository.Setup(r => r.Load(0)).Returns(roomData);

            var mockTileBuilder = new Mock<ITileBuilder>();

            var roombuilder = new RoomBuilder(mockTileBuilder.Object, mockRoomRepository.Object);

            // When

            roombuilder.Build(new RoomInChunk(0, new ChunkPosition(0, 0)));

            // Then

            mockTileBuilder.Verify(b => b.PlaceGround(It.IsAny<TilePosition>()), Times.Exactly(2), $"Incorrect number of ground placed!");
        }

        [Test]
        public void When_A_Room_Is_Built_Then_An_Event_Is_Raised()
        {
            // Given

            var roomData = new RoomData();
            var mockRoomRepository = new Mock<IRoomRepository>();
            mockRoomRepository.Setup(r => r.Load(0)).Returns(roomData);

            var mockTileBuilder = new Mock<ITileBuilder>();

            var roombuilder = new RoomBuilder(mockTileBuilder.Object, mockRoomRepository.Object);

            // When

            var raisedArgs = ListenFor
                .First<RoomBuiltEventArgs>()
                .From(roombuilder)
                .During(() => roombuilder.Build(new RoomInChunk(0, new ChunkPosition(0, 0))));

            // Then

            Assert.That(raisedArgs, Is.Not.Null, "No was event raised!");
        }

        #endregion

    }

}