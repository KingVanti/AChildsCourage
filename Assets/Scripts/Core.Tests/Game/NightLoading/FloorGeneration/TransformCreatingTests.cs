using AChildsCourage.Game.Floors;
using NUnit.Framework;
using static AChildsCourage.Game.NightLoading.FloorGenerating;

namespace AChildsCourage.Game.NightLoading
{

    [TestFixture]
    public class TransformCreatingTests
    {

        [Test]
        public void Can_Get_Correct_Chunk_Transform_From_Room_Transform()
        {
            // Given

            var roomTransform = new RoomTransform(new ChunkPosition(1, 1), true, 1);

            // When

            var chunkTransform = ToChunkTransform(roomTransform);

            // Then

            Assert.That(chunkTransform.RotationCount, Is.EqualTo(1), "Rotation incorrectly copied!");
            Assert.That(chunkTransform.IsMirrored, Is.True, "IsMirrored incorrectly copied!");
            Assert.That(chunkTransform.ChunkCenter, Is.EqualTo(new MTilePosition.TilePosition(31, 31)), "Center incorrectly copied!");
            Assert.That(chunkTransform.ChunkCorner, Is.EqualTo(new MTilePosition.TilePosition(21, 21)), "Corner incorrectly copied!");
        }


        [Test]
        public void Center_For_Chunks_Are_Calculated_Correctly()
        {
            // Given

            var chunkPosition = new ChunkPosition(1, -1);

            // When

            var center = GetChunkCenter(chunkPosition);

            // Then

            Assert.That(center, Is.EqualTo(new MTilePosition.TilePosition(31, -11)), "Center calculated incorrectly!");
        }


        [Test]
        public void Corner_For_Chunks_Are_Calculated_Correctly()
        {
            // Given

            var chunkPosition = new ChunkPosition(1, -1);

            // When

            var corner = GetChunkCorner(chunkPosition);

            // Then

            Assert.That(corner, Is.EqualTo(new MTilePosition.TilePosition(21, -21)), "Corner calculated incorrectly!");
        }

    }

}