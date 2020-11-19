using AChildsCourage.Game.Floors;
using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.NightManagement.Loading.FloorGenerationUtility;
using static AChildsCourage.Game.NightManagement.Loading.FloorPlanGenerating;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class FloorGenerationUtilityTests
    {

        #region Tests

        [Test]
        public void Moving_To_Adjacent_Chunk_Returns_Correct_Chunk()
        {
            // Given

            var chunkPosition = new ChunkPosition(0, 0);

            // When

            var toNorth = MoveToAdjacentChunk(chunkPosition, PassageDirection.North);
            var toEast = MoveToAdjacentChunk(chunkPosition, PassageDirection.East);
            var toSouth = MoveToAdjacentChunk(chunkPosition, PassageDirection.South);
            var toWest = MoveToAdjacentChunk(chunkPosition, PassageDirection.West);

            // Then

            Assert.That(toNorth, Is.EqualTo(new ChunkPosition(0, 1)), "Moving north produced incorrect result!");
            Assert.That(toEast, Is.EqualTo(new ChunkPosition(1, 0)), "Moving east produced incorrect result!");
            Assert.That(toSouth, Is.EqualTo(new ChunkPosition(0, -1)), "Moving south produced incorrect result!");
            Assert.That(toWest, Is.EqualTo(new ChunkPosition(-1, 0)), "Moving west produced incorrect result!");
        }


        [Test]
        [TestCase(PassageDirection.North, PassageDirection.South)]
        [TestCase(PassageDirection.East, PassageDirection.West)]
        [TestCase(PassageDirection.South, PassageDirection.North)]
        [TestCase(PassageDirection.West, PassageDirection.East)]
        public void Passages_Are_Inverted_Correctly(PassageDirection passage, PassageDirection expected)
        {
            // When

            var actual = Invert(passage);

            // Then

            Assert.That(actual, Is.EqualTo(expected), "Passage inverted incorrectly!");
        }


        [Test]
        public void When_A_Builder_Has_No_Rooms_Built_Then_It_Is_In_The_StartRoom_Phase()
        {
            // When

            var phase = GetCurrentPhase(0);

            // Then

            Assert.That(phase, Is.EqualTo(GenerationPhase.StartRoom), "Builder should be in StartRoom phase!");
        }

        [Test]
        public void When_A_Builder_Has_All_But_One_Rooms_Built_Then_It_Is_In_The_EndRoom_Phase()
        {
            // When

            var phase = GetCurrentPhase(GoalRoomCount - 1);

            // Then

            Assert.That(phase, Is.EqualTo(GenerationPhase.EndRoom), "Builder should be in EndRoom phase!");
        }

        [Test]
        public void When_A_Builder_Is_Neither_In_The_StartRoom_Nor_EndRoom_Phase_Then_It_Is_In_The_NormalRooms_Phase()
        {
            // When

            var phases =
                Enumerable.Range(1, GoalRoomCount - 2)
                .Select(currentRoomCount => GetCurrentPhase(currentRoomCount));

            // Then

            Assert.That(phases, Is.All.EqualTo(GenerationPhase.NormalRooms), "Builder should be in NormalRoom phase!");
        }


        [Test]
        public void When_There_Is_No_Room_At_A_Given_Chunk_Then_It_Is_Empty()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var isEmpty = IsEmpty(builder, new ChunkPosition(0, 0));

            // Then

            Assert.That(isEmpty, Is.True, "The chunk should be empty!");
        }


        [Test]
        public void When_There_Is_A_Room_At_A_Given_Chunk_Then_It_Is_Not_Empty()
        {
            // Given

            var builder = new FloorPlanBuilder();
            builder.RoomsByChunks.Add(new ChunkPosition(0, 0), new RoomPassages());

            // When

            var isEmpty = IsEmpty(builder, new ChunkPosition(0, 0));

            // Then

            Assert.That(isEmpty, Is.False, "The chunk should not be empty!");
        }


        [Test]
        public void Room_Count_Is_Calculated_Correctly()
        {
            // Given

            var builder = new FloorPlanBuilder();

            builder.RoomsByChunks.Add(new ChunkPosition(0, 0), new RoomPassages());
            builder.RoomsByChunks.Add(new ChunkPosition(1, 0), new RoomPassages());

            // When

            var roomCount = CountRooms(builder);

            // Then

            Assert.That(roomCount, Is.EqualTo(2), "Room-count calculated incorrectly!");
        }

        #endregion

    }

}