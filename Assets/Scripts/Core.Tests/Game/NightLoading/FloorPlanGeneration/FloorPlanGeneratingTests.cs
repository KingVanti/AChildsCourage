using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.NightLoading.FloorPlanGenerating;

namespace AChildsCourage.Game.NightLoading
{

    [TestFixture]
    public class FloorPlanGeneratingTests
    {

        [Test]
        public void Needs_More_Rooms_While_Room_Count_Is_Not_Enough()
        {
            // Given

            var floorPlan = new FloorPlanInProgress();

            // When

            var needsMoreRooms = NeedsMoreRooms(floorPlan);

            // Then

            Assert.That(needsMoreRooms, Is.True, "Should need more rooms!");
        }

        [Test]
        public void Needs_No_More_Rooms_When_Room_Count_Is_Enough()
        {
            // Given

            var floorPlan = new FloorPlanInProgress();
            Enumerable.Range(0, GoalRoomCount).ForEach(i => floorPlan.RoomsByChunks.Add(new ChunkPosition(i, 0), null));

            // When

            var needsMoreRooms = NeedsMoreRooms(floorPlan);

            // Then

            Assert.That(needsMoreRooms, Is.False, "Should not need more rooms!");
        }


        [Test]
        public void Less_Than_Goal_Room_Count_Is_Not_Enough()
        {
            // When

            var isEnough =
                Enumerable.Range(0, GoalRoomCount - 1)
                .Select(roomCount =>
                {
                    return IsEnough(roomCount);
                });

            // Then

            Assert.That(isEnough, Is.All.False, "Should not be enough!");
        }

        [Test]
        public void Equal_To_Goal_Room_Count_Is_Enough()
        {
            // When

            var isEnough = IsEnough(GoalRoomCount);

            // Then

            Assert.That(isEnough, Is.True, "Should be enough!");
        }

    }

}