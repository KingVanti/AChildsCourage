using Moq;
using NUnit.Framework;
using System.Linq;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class GenerationSessionTests
    {

        #region Data

        private static RoomPassages StartRoom { get; } = new RoomPassages(0, ChunkPassages.All);

        #endregion

        #region Tests

        [Test]
        public void Starting_Room_Is_Always_At_Origin()
        {
            // Given

            var mockRoomInfoRepository = new Mock<IRoomPassagesRepository>();
            mockRoomInfoRepository.SetupGet(r => r.StartRoom).Returns(StartRoom);

            var chunkGrid = new ChunkGrid();

            var session = new GenerationSession(new RNG(0), chunkGrid, mockRoomInfoRepository.Object);

            // When

            session.PlaceStartRoom();

            // Then

            var floorPlan = chunkGrid.BuildPlan();
            var originRoom = floorPlan.Rooms.FirstOrDefault(r => r.Position == new ChunkPosition(0, 0));

            Assert.That(originRoom, Is.Not.Null, "No room placed at origin");
            Assert.That(originRoom.RoomId, Is.Zero, "Incorrect room placed at origin!");
        }

        #endregion

    }

}