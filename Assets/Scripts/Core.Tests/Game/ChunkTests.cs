using NUnit.Framework;
using static AChildsCourage.Game.Chunk;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class ChunkTests
    {

        [Test]
        public void Chunk_Centers_Are_In_The_Correct_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunk = new Chunk(x, y);

            // When

            var center = chunk.Map(GetCenter);

            // Then

            var newChunk = center.Map(GetChunk);
            Assert.That(newChunk, Is.EqualTo(chunk), "Center is in different chunk!");
        }

        [Test]
        public void Chunk_Centers_Are_In_Center_Of_A_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunk = new Chunk(x, y);

            // When

            var center = chunk.Map(GetCenter);

            // Then

            Assert.That((center.X - ChunkExtent) % ChunkSize, Is.EqualTo(0), $"Tile {center} x is not in chunk center!");
            Assert.That((center.Y - ChunkExtent) % ChunkSize, Is.EqualTo(0), $"Tile {center} y is not in chunk center!");
        }

        [Test]
        public void Chunk_Corners_Are_In_The_Correct_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunk = new Chunk(x, y);

            // When

            var corner = chunk.Map(GetCorner);

            // Then

            var newChunk = corner.Map(GetChunk);
            Assert.That(newChunk, Is.EqualTo(chunk), "Corner is in different chunk!");
        }

        [Test]
        public void Chunk_Corners_Are_In_Corner_Of_A_Chunk([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunk = new Chunk(x, y);

            // When

            var corner = chunk.Map(GetCorner);

            // Then

            Assert.That(corner.X % ChunkSize, Is.EqualTo(0), $"Tile {corner} x is not in chunk corner!");
            Assert.That(corner.Y % ChunkSize, Is.EqualTo(0), $"Tile {corner} y is not in chunk corner!");
        }

        [Test]
        public void Absolute_Chunk_Has_Same_Distance_To_Origin_As_Original([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunk = new Chunk(x, y);

            // When

            var absolute = chunk.Map(Absolute);

            // Then

            Assert.That(absolute.Map(GetDistanceToOrigin), Is.EqualTo(chunk.Map(GetDistanceToOrigin)), "Chunks dont have same distance to origin!");
        }

        [Test]
        public void Absolute_Chunk_Has_Only_Positive_Coordinates([Random(-10, 10, 10)] int x, [Random(-10, 10, 10)] int y)
        {
            // Given

            var chunk = new Chunk(x, y);

            // When

            var absolute = chunk.Map(Absolute);

            // Then

            Assert.That(absolute.X, Is.GreaterThanOrEqualTo(0), "X is not positive!");
            Assert.That(absolute.Y, Is.GreaterThanOrEqualTo(0), "Y is not positive!");
        }

    }

}