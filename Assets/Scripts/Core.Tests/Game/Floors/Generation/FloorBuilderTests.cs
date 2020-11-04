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

            var mockRoomBuilder = new Mock<IRoomBuilder>();

            var floorPlan = new FloorPlan(new[]
            {
                new RoomInChunk(0, new ChunkPosition(0, 0)),
                new RoomInChunk(1, new ChunkPosition(1, 1))
            });
            var floorBuilder = new FloorBuilder(mockRoomBuilder.Object);

            // When

            floorBuilder.Build(floorPlan);

            // Then

            foreach (var roomPosition in floorPlan.Rooms)
                mockRoomBuilder.Verify(b => b.Build(roomPosition), Times.Once, $"Room position {roomPosition} was not built!");
        }

        #endregion

    }

}