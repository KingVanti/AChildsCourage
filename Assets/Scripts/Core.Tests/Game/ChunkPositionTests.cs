using NUnit.Framework;
using static AChildsCourage.Game.ChunkPosition;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class ChunkPositionTests
    {

        [Test]
        public void Chunk_Centers_Are_In_The_Correct_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunkPosition = new ChunkPosition(x, y);

            // When

            var center = chunkPosition.Map(GetCenter);

            // Then

            var newChunkPosition = center.Map(GetChunk);
            Assert.That(newChunkPosition, Is.EqualTo(chunkPosition), "Center is in different chunk!");
        }

        [Test]
        public void Chunk_Centers_Are_In_Center_Of_A_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunkPosition = new ChunkPosition(x, y);

            // When

            var center = chunkPosition.Map(GetCenter);

            // Then

            Assert.That((center.X - ChunkExtent) % ChunkSize, Is.EqualTo(0), $"Tile {center} x is not in chunk center!");
            Assert.That((center.Y - ChunkExtent) % ChunkSize, Is.EqualTo(0), $"Tile {center} y is not in chunk center!");
        }

        [Test]
        public void Chunk_Corners_Are_In_The_Correct_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunkPosition = new ChunkPosition(x, y);

            // When

            var corner = chunkPosition.Map(GetCorner);

            // Then

            var newChunkPosition = corner.Map(GetChunk);
            Assert.That(newChunkPosition, Is.EqualTo(chunkPosition), "Corner is in different chunk!");
        }

        [Test]
        public void Chunk_Corners_Are_In_Corner_Of_A_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunkPosition = new ChunkPosition(x, y);

            // When

            var corner = chunkPosition.Map(GetCorner);

            // Then

            Assert.That(corner.X % ChunkSize, Is.EqualTo(0), $"Tile {corner} x is not in chunk corner!");
            Assert.That(corner.Y % ChunkSize, Is.EqualTo(0), $"Tile {corner} y is not in chunk corner!");
        }

    }

}