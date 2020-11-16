using AChildsCourage.Game.Floors.Persistance;
using Moq;
using NUnit.Framework;
using PADEAH.Tabs;

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

        #endregion

        #region Helpers

        private IRoomRepository GetRoomRepositoryWith(params RoomInChunk[] roomsInChunks)
        {
            var mockRoomRepository = new Mock<IRoomRepository>();

            mockRoomRepository.Setup(r => r.LoadRoomsFor(It.IsAny<FloorPlan>())).Returns(new RoomsInChunks(roomsInChunks));

            return mockRoomRepository.Object;
        }


        #endregion

    }

}