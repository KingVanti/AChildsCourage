using NUnit.Framework;
using static AChildsCourage.Game.NightLoading.FloorPlanGenerating;

namespace AChildsCourage.Game.NightLoading
{

    [TestFixture]
    public class ChunkChoosingTests
    {

        [Test]
        public void The_Start_Room_Is_Always_At_Origin()
        {
            // When

            var startRoomChunk = GetStartRoomChunk();

            // Then

            Assert.That(startRoomChunk, Is.EqualTo(new ChunkPosition(0, 0)), "Start-room should be at origin!");
        }


        [Test]
        public void Reserved_Chunks_Are_Identified_Correctly()
        {
            // Given

            var floorPlan = new FloorPlanInProgress();
            floorPlan.ReservedChunks.Add(new ChunkPosition(0, 0));

            // When

            var hasReserver = HasReservedChunks(floorPlan);

            // Then

            Assert.That(hasReserver, Is.True, "Builder should have reserved chunks!");
        }

        [Test]
        public void Absence_Of_Reserved_Chunks_Is_Identified_Correctly()
        {
            // Given

            var floorPlan = new FloorPlanInProgress();

            // When

            var hasReserver = HasReservedChunks(floorPlan);

            // Then

            Assert.That(hasReserver, Is.False, "Builder should not have reserved chunks!");
        }


        [Test]
        public void Chunks_Have_A_Higher_Weight_The_Closer_They_Are_To_The_Origin()
        {
            // Given

            var chunkPos1 = new ChunkPosition(1, 1);
            var chunkPos2 = new ChunkPosition(2, 2);

            // When

            var weight1 = GetChunkWeight(chunkPos1);
            var weight2 = GetChunkWeight(chunkPos2);

            // Then

            Assert.That(weight1, Is.GreaterThan(weight2), "Closer chunk should have greater weight!");
        }

    }

}