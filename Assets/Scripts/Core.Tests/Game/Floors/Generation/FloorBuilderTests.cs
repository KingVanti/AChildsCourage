using AChildsCourage.Game.Floors.Persistance;
using Moq;
using NUnit.Framework;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class FloorBuilderTests
    {

        #region Tests

        [Test]
        public void When_A_Floor_Is_Built_Then_All_Its_Rooms_Are_Built()
        {
            // Given

            var floorPlan = new FloorPlan(new[]
            {
                new RoomInChunk(0, new ChunkPosition(0, 0)),
                new RoomInChunk(1, new ChunkPosition(1, 1))
            });

            var floorRooms = new RoomsAtPositions()
            {
                new RoomAtPosition(new ChunkPosition(0, 0), null),
                new RoomAtPosition(new ChunkPosition(1, 1), null)
            };

            var mockRoomRepository = new Mock<IRoomRepository>();

            mockRoomRepository.Setup(r => r.LoadRoomsFor(floorPlan)).Returns(floorRooms);

            var mockRoomBuilder = new Mock<IRoomBuilder>();

            var floorBuilder = new FloorBuilder(mockRoomRepository.Object, mockRoomBuilder.Object);

            // When

            floorBuilder.Build(floorPlan);

            // Then

            foreach (var room in floorRooms)
                mockRoomBuilder.Verify(b => b.Build(room.Room, room.Position), Times.Once, $"Room position { room.Position } was not built!");
        }

        #endregion

    }

}