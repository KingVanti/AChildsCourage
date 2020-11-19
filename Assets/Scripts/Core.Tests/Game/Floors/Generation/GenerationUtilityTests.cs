using NUnit.Framework;
using System.Linq;
using System.Numerics;

using static AChildsCourage.Game.Floors.FloorGeneration;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class GenerationUtilityTests
    {

        #region Tests

        [Test]
        public void Moving_To_Adjacent_Chunk_Returns_Correct_Chunk()
        {
            // Given

            var chunkPosition = new ChunkPosition(0, 0);

            // When

            var toNorth = chunkPosition.MoveToAdjacentChunk(PassageDirection.North);
            var toEast = chunkPosition.MoveToAdjacentChunk(PassageDirection.East);
            var toSouth = chunkPosition.MoveToAdjacentChunk(PassageDirection.South);
            var toWest = chunkPosition.MoveToAdjacentChunk(PassageDirection.West);

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

            var actual = passage.Invert();

            // Then

            Assert.That(actual, Is.EqualTo(expected), "Passage inverted incorrectly!");
        }


        [Test]
        public void When_A_Builder_Has_No_Rooms_Built_Then_It_Is_In_The_StartRoom_Phase()
        {
            // When

            var phase = 0.GetCurrentPhase();

            // Then

            Assert.That(phase, Is.EqualTo(GenerationPhase.StartRoom), "Builder should be in StartRoom phase!");
        }

        [Test]
        public void When_A_Builder_Has_All_But_One_Rooms_Built_Then_It_Is_In_The_EndRoom_Phase()
        {
            // When

            var phase = (GoalRoomCount - 1).GetCurrentPhase();

            // Then

            Assert.That(phase, Is.EqualTo(GenerationPhase.EndRoom), "Builder should be in EndRoom phase!");
        }

        [Test]
        public void When_A_Builder_Is_Neither_In_The_StartRoom_Nor_EndRoom_Phase_Then_It_Is_In_The_NormalRooms_Phase()
        {
            // When

            var phases =
                Enumerable.Range(1, GoalRoomCount - 2)
                .Select(currentRoomCount => currentRoomCount.GetCurrentPhase());

            // Then

            Assert.That(phases, Is.All.EqualTo(GenerationPhase.NormalRooms), "Builder should be in NormalRoom phase!");
        }


        #endregion

    }

}