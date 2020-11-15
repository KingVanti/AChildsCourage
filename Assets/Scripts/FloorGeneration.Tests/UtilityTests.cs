using NUnit.Framework;
using System.Linq;
using System.Numerics;

using static AChildsCourage.Game.Floors.FloorGenerationModule;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class UtilityTests
    {

        #region Tests

        [Test]
        public void Get_Correct_Surrounding_Positions()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            // When

            var surrounding = GetSurroundingPositions(position).ToArray();

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

            var toNorth = MoveToAdjacentChunk(chunkPosition, Passage.North);
            var toEast = MoveToAdjacentChunk(chunkPosition, Passage.East);
            var toSouth = MoveToAdjacentChunk(chunkPosition, Passage.South);
            var toWest = MoveToAdjacentChunk(chunkPosition, Passage.West);

            // Then

            Assert.That(toNorth, Is.EqualTo(new ChunkPosition(0, 1)), "Moving north produced incorrect result!");
            Assert.That(toEast, Is.EqualTo(new ChunkPosition(1, 0)), "Moving east produced incorrect result!");
            Assert.That(toSouth, Is.EqualTo(new ChunkPosition(0, -1)), "Moving south produced incorrect result!");
            Assert.That(toWest, Is.EqualTo(new ChunkPosition(-1, 0)), "Moving west produced incorrect result!");
        }


        [Test]
        [TestCase(Passage.North, Passage.South)]
        [TestCase(Passage.East, Passage.West)]
        [TestCase(Passage.South, Passage.North)]
        [TestCase(Passage.West, Passage.East)]
        public void Passages_Are_Inverted_Correctly(Passage passage, Passage expected)
        {
            // When

            var actual = passage.Invert();

            // Then

            Assert.That(actual, Is.EqualTo(expected), "Passage inverted incorrectly!");
        }

        #endregion

    }

}