using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.Floors.FloorGenerationModule;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class FilterCreatingTests
    {

        #region Tests

        [Test]
        [TestCase(GenerationPhase.StartRoom, RoomType.Start)]
        [TestCase(GenerationPhase.NormalRooms, RoomType.Normal)]
        [TestCase(GenerationPhase.EndRoom, RoomType.End)]
        public void Generation_Phases_Are_Converted_To_Correct_Room_Types(GenerationPhase phase, RoomType expected)
        {
            // When

            var actual = GetFilteredRoomTypeFor(phase);

            // Then

            Assert.That(actual, Is.EqualTo(expected), "Incorrect room type!");
        }

        [Test]
        public void Remaining_Room_Count_Is_Calculated_Correctly()
        {
            // Given

            var builder = new FloorPlanBuilder();

            builder.RoomsByChunks.Add(new ChunkPosition(0, 0), new RoomPassages());
            builder.RoomsByChunks.Add(new ChunkPosition(1, 0), new RoomPassages());
            builder.RoomsByChunks.Add(new ChunkPosition(2, 0), new RoomPassages());
            builder.ReservedChunks.AddRange(Enumerable.Repeat(new ChunkPosition(0, 0), 4));

            // When

            var remainingRoomCount = builder.GetRemainingRoomCount();

            // Then

            Assert.That(remainingRoomCount, Is.EqualTo(8), "Remaing room-count calculated incorrectly!");
        }


        #endregion

    }

}
