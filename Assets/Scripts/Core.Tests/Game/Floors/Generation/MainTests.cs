using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.Floors.FloorGeneration;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class MainTests
    {
        [Test]
        public void The_Builder_Needs_Rooms_While_The_Number_Of_Built_Rooms_Is_Smaller_Than_The_Goal_Room_Count()
        {
            // When

            var isEnough =
                Enumerable.Range(0, GoalRoomCount - 1)
                .Select(roomCount =>
                {
                    return roomCount.IsEnough();
                });

            // Then

            Assert.That(isEnough, Is.All.False, "Should not be enough!");
        }

        [Test]
        public void The_Builder_Needs_No_Rooms_When_The_Number_Of_Built_Rooms_Is_Equal_To_The_Goal_Room_Count()
        {
            // When

            var isEnough = GoalRoomCount.IsEnough();

            // Then

            Assert.That(isEnough, Is.True, "Should be enough!");
        }

    }

}