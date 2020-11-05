using NUnit.Framework;
using System.Linq;
using System.Numerics;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class GenerationUtilityTests
    {

        #region Tests

        [TestCase(Passages.North, Passages.North, false)]
        [TestCase(Passages.North, Passages.East, false)]
        [TestCase(Passages.North, Passages.South, true)]
        [TestCase(Passages.North, Passages.West, false)]

        [TestCase(Passages.East, Passages.North, false)]
        [TestCase(Passages.East, Passages.East, false)]
        [TestCase(Passages.East, Passages.South, false)]
        [TestCase(Passages.East, Passages.West, true)]

        [TestCase(Passages.South, Passages.North, true)]
        [TestCase(Passages.South, Passages.East, false)]
        [TestCase(Passages.South, Passages.South, false)]
        [TestCase(Passages.South, Passages.West, false)]

        [TestCase(Passages.West, Passages.North, false)]
        [TestCase(Passages.West, Passages.East, true)]
        [TestCase(Passages.West, Passages.South, false)]
        [TestCase(Passages.West, Passages.West, false)]
        public void Directions_Connect_Correctly(Passages first, Passages second, bool excpected)
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