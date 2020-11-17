using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.Floors.FloorGeneration;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class FloorPlanCreationTests
    {

        #region Tests

        [Test]
        public void When_A_FloorPlan_Is_Created_Then_It_Has_The_Same_Rooms_As_The_Builder()
        {
            // Given

            var position = new ChunkPosition(0, 0);
            var room = new RoomPassages(1, ChunkPassages.None);

            var builder = new FloorPlanBuilder();
            builder.PlaceRoom(position, room);

            // When

            var floorPlan = builder.GetFloorPlan();

            // Then

            Assert.That(floorPlan.Rooms.Length, Is.EqualTo(1), "Incorrect number of rooms!");
            Assert.That(floorPlan.Rooms.First().RoomId, Is.EqualTo(room.RoomId), "Room id incorrectly copied!");
            Assert.That(floorPlan.Rooms.First().Transform.Position, Is.EqualTo(position), "Position incorrectly copied!");
        }

        #endregion

    }

}
