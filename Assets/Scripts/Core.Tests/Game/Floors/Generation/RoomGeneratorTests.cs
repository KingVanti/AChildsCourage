using AChildsCourage.Game.Floors.Persistance;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class RoomGeneratorTests
    {

        #region Data

        private static TilePositions TestWallPositions { get; } = new TilePositions(new[]
        {
            new TilePosition(0, 0),
            new TilePosition(1, 0),
            new TilePosition(2, 0),
            new TilePosition(3, 0)
        });

        private static TilePositions TestFloorPositions { get; } = new TilePositions(new[]
        {
            new TilePosition(0, 1),
            new TilePosition(1, 1),
            new TilePosition(2, 1),
            new TilePosition(3, 1)
        });

        private static RoomShape TestRoomShape { get; } = new RoomShape(TestWallPositions, TestFloorPositions);

        private static TilePositions TestItemPositions { get; } = new TilePositions(new[]
        {
            new TilePosition(0, 1)
        });

        private static TilePositions TestSmallCouragePositions { get; } = new TilePositions(new[]
        {
            new TilePosition(1, 1)
        });

        private static TilePositions TestBigCouragePositions { get; } = new TilePositions(new[]
        {
            new TilePosition(2, 1)
        });

        private static RoomEntities TestRoomEntities { get; } = new RoomEntities(TestItemPositions, TestSmallCouragePositions, TestBigCouragePositions);

        private static RoomData TestRoomData { get; } = new RoomData(TestRoomShape, TestRoomEntities);

        #endregion

        #region Tests

        [Test]
        public void Given_Any_RoomGenerator_When_It_Generates_A_Room_Then_It_Starts_Building_A_Room()
        {
            // Given

            var mockRoomBuilder = new Mock<IRoomBuilder>();
            var roomGenerator = new RoomGenerator(mockRoomBuilder.Object);

            // When

            roomGenerator.GenerateFrom(TestRoomData);

            // Then

            mockRoomBuilder.Verify(b => b.StartBuilding());
        }

        [Test]
        public void Given_Any_RoomGenerator_When_It_Generates_A_Room_Then_Places_All_Walls()
        {
            // Given

            var mockRoomBuilder = new Mock<IRoomBuilder>();
            var roomGenerator = new RoomGenerator(mockRoomBuilder.Object);

            var builtWallPositions = new List<TilePosition>();
            mockRoomBuilder
                .Setup(b => b.PlaceWall(It.IsAny<TilePosition>(), It.IsAny<RoomBuildingSession>()))
                .Callback<TilePosition, RoomBuildingSession>((p, _) => builtWallPositions.Add(p));

            // When

            roomGenerator.GenerateFrom(TestRoomData);

            // Then

            Assert.That(builtWallPositions, Is.EqualTo(TestWallPositions));
        }

        [Test]
        public void Given_Any_RoomGenerator_When_It_Generates_A_Room_Then_Places_All_Floors()
        {
            // Given

            var mockRoomBuilder = new Mock<IRoomBuilder>();
            var roomGenerator = new RoomGenerator(mockRoomBuilder.Object);

            var builtFloorPositions = new List<TilePosition>();
            mockRoomBuilder
                .Setup(b => b.PlaceFloor(It.IsAny<TilePosition>(), It.IsAny<RoomBuildingSession>()))
                .Callback<TilePosition, RoomBuildingSession>((p, _) => builtFloorPositions.Add(p));

            // When

            roomGenerator.GenerateFrom(TestRoomData);

            // Then

            Assert.That(builtFloorPositions, Is.EqualTo(TestFloorPositions));
        }

        [Test]
        public void Given_Any_RoomGenerator_When_It_Generates_A_Room_Then_A_Room_Is_Returned()
        {
            // Given

            var mockRoomBuilder = new Mock<IRoomBuilder>();
            var roomGenerator = new RoomGenerator(mockRoomBuilder.Object);

            // When

            var room = roomGenerator.GenerateFrom(TestRoomData);

            // Then

            Assert.That(room, Is.Not.Null);
        }

        #endregion

    }

}