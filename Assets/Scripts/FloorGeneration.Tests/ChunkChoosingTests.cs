using NUnit.Framework;
using static AChildsCourage.Game.Floors.FloorGeneration;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class ChunkChoosingTests
    {

        #region Tests

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


        [Test]
        public void The_Start_Room_Is_Always_At_Origin()
        {
            // When

            var startRoomChunk = GetStartRoomChunk();

            // Then

            Assert.That(startRoomChunk, Is.EqualTo(new ChunkPosition(0, 0)), "Start-room should be at origin!");
        }

        #endregion
    }

}