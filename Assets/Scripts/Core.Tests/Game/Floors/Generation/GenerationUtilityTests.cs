using NUnit.Framework;
using System.Linq;
using System.Numerics;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class GenerationUtilityTests
    {

        #region Tests

        [TestCase(Passage.North, Passage.North, false)]
        [TestCase(Passage.North, Passage.East, false)]
        [TestCase(Passage.North, Passage.South, true)]
        [TestCase(Passage.North, Passage.West, false)]

        [TestCase(Passage.East, Passage.North, false)]
        [TestCase(Passage.East, Passage.East, false)]
        [TestCase(Passage.East, Passage.South, false)]
        [TestCase(Passage.East, Passage.West, true)]

        [TestCase(Passage.South, Passage.North, true)]
        [TestCase(Passage.South, Passage.East, false)]
        [TestCase(Passage.South, Passage.South, false)]
        [TestCase(Passage.South, Passage.West, false)]

        [TestCase(Passage.West, Passage.North, false)]
        [TestCase(Passage.West, Passage.East, true)]
        [TestCase(Passage.West, Passage.South, false)]
        [TestCase(Passage.West, Passage.West, false)]
        public void Directions_Connect_Correctly(Passage first, Passage second, bool excpected)
        {
            // When

            var actual = GenerationUtility.CanConnect(first, second);

            // Then

            Assert.That(actual, Is.EqualTo(excpected));
        }
    

        [Test]
        public void Get_Correct_Surrounding_Positions()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            // When

            var surrounding = GenerationUtility.GetSurroundingPositions(position).ToArray();

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

        #endregion

    }

}