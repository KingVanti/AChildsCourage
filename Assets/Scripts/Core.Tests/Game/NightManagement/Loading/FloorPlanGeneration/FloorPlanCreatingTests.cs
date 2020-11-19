using AChildsCourage.Game.Floors;
using NUnit.Framework;
using static AChildsCourage.Game.NightManagement.Loading.FloorPlanCreating;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class FloorPlanCreatingTests
    {

        #region Tests

        [Test]
        public void Creating_A_Floor_Plan_Creates_Rooms_For_All_Positions_And_Converts_Them_To_A_Floor_Plan()
        {
            // Given

            var positions = new[]
            {
                new ChunkPosition(0, 0),
                new ChunkPosition(1, 0)
            };

            RoomPlanCreator roomPlanCreator = pos => new RoomPlan(0, new RoomTransform(pos, false, 0));

            // When

            var floorPlan = CreateFloorPlan(positions, roomPlanCreator);

            // Then

            var expected = new[]
            {
                new RoomPlan(0, new RoomTransform(new ChunkPosition(0, 0), false, 0)),
                new RoomPlan(0, new RoomTransform(new ChunkPosition(1, 0), false, 0))
            };
            Assert.That(floorPlan.Rooms, Is.EqualTo(expected), "Floorplan incorrectly created!");
        }


        [Test]
        public void Converting_To_A_Floor_Plan_Copies_The_Given_Room_Plans()
        {
            // Given

            var roomPlans = new[]
            {
                new RoomPlan(0, new RoomTransform(new ChunkPosition(0, 0), false, 1)),
                new RoomPlan(1, new RoomTransform(new ChunkPosition(1, 0), false, 1)),
            };

            // When

            var floorPlan = ConvertToFloorPlan(roomPlans);

            // Then

            Assert.That(floorPlan.Rooms, Is.EqualTo(roomPlans), "Roomplans incorrectly copied!");
        }

        #endregion

    }

}
