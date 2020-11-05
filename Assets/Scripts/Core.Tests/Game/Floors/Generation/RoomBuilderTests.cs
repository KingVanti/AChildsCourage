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
            var roomPosition = new TilePosition(1, 1);
            mockRoomRepository.Setup(r => r.Load(0)).Returns(roomData);

            var mockTileBuilder = new Mock<ITileBuilder>();

            var roombuilder = new RoomBuilder(mockTileBuilder.Object, mockRoomRepository.Object);

            // When

            roombuilder.Build(new RoomAtPosition(0, roomPosition));

            // Then

            foreach (var wallPosition in roomData.WallPositions)
            {
                var offsetPosition = wallPosition + roomPosition;

                mockTileBuilder.Verify(b => b.PlaceWall(offsetPosition), Times.Once, $"No wall placed at {offsetPosition}!");
            }
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
            var roomPosition = new TilePosition(1, 1);
            mockRoomRepository.Setup(r => r.Load(0)).Returns(roomData);

            var mockTileBuilder = new Mock<ITileBuilder>();

            var roombuilder = new RoomBuilder(mockTileBuilder.Object, mockRoomRepository.Object);

            // When

            roombuilder.Build(new RoomAtPosition(0, roomPosition));

            // Then

            foreach (var groundPosition in roomData.GroundPositions)
            {
                var offsetPosition = groundPosition + roomPosition;

                mockTileBuilder.Verify(b => b.PlaceGround(offsetPosition), Times.Once, $"No ground placed at {offsetPosition}!");
            }
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
                .During(() => roombuilder.Build(new RoomAtPosition(0, new TilePosition(0, 0))));

            // Then

            Assert.That(raisedArgs, Is.Not.Null, "No was event raised!");
        }

        #endregion

    }

}