using NUnit.Framework;
using System.Linq;
using System.Numerics;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class GenerationTests
    {

        #region Tests

        [Test]
        public void Tile_Offset_Is_Chunk_Position_Times_Chunk_Size()
        {
            // Given

            var chunkPosition = new ChunkPosition(1, -1);

            // When

            var offset = Generation.GetTileOffsetFor(chunkPosition);

            // Then

            Assert.That(offset.X, Is.EqualTo(41), "X coordinate incorrect!");
            Assert.That(offset.Y, Is.EqualTo(-41), "Y coordinate incorrect!");
        }

        [Test]
        public void Get_Correct_Surrounding_Positions()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            // When

            var surrounding = Generation.GetSurroundingPositions(position).ToArray();

            // Then

            Assert.That(surrounding.Length, Is.EqualTo(4), "Wrong number of positions!");
            Assert.That(surrounding.Distinct().Count(), Is.EqualTo(surrounding.Length), "Positions should be destinct!");

            for (var i = 0; i < 4; i++)
            {
                var p = surrounding[i];

                var diff = new Vector2(p.X - position.X, p.Y - position.Y);

                Assert.That(diff.Length(), Is.EqualTo(1), "Distance of position to origin should be 1!");
            }
        }

        [Test]
        public void Moving_To_Adjacent_Chunk_Returns_Correct_Chunk()
        {
            // Given

            var chunkPosition = new ChunkPosition(0, 0);

            // When

            var toNorth = Generation.MoveToAdjacentChunk(chunkPosition, Passage.North);
            var toEast = Generation.MoveToAdjacentChunk(chunkPosition, Passage.East);
            var toSouth = Generation.MoveToAdjacentChunk(chunkPosition, Passage.South);
            var toWest = Generation.MoveToAdjacentChunk(chunkPosition, Passage.West);

            // Then

            Assert.That(toNorth, Is.EqualTo(new ChunkPosition(0, 1)), "Moving north produced incorrect result!");
            Assert.That(toEast, Is.EqualTo(new ChunkPosition(1, 0)), "Moving east produced incorrect result!");
            Assert.That(toSouth, Is.EqualTo(new ChunkPosition(0, -1)), "Moving south produced incorrect result!");
            Assert.That(toWest, Is.EqualTo(new ChunkPosition(-1, 0)), "Moving west produced incorrect result!");
        }

        #endregion

    }

}