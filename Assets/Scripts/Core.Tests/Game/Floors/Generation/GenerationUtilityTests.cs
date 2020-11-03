using NUnit.Framework;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class GenerationUtilityTests 
    {

        #region Tests

        [TestCase(PassageDirection.North, PassageDirection.North, false)]
        [TestCase(PassageDirection.North, PassageDirection.East, false)]
        [TestCase(PassageDirection.North, PassageDirection.South, true)]
        [TestCase(PassageDirection.North, PassageDirection.West, false)]

        [TestCase(PassageDirection.East, PassageDirection.North, false)]
        [TestCase(PassageDirection.East, PassageDirection.East, false)]
        [TestCase(PassageDirection.East, PassageDirection.South, false)]
        [TestCase(PassageDirection.East, PassageDirection.West, true)]

        [TestCase(PassageDirection.South, PassageDirection.North, true)]
        [TestCase(PassageDirection.South, PassageDirection.East, false)]
        [TestCase(PassageDirection.South, PassageDirection.South, false)]
        [TestCase(PassageDirection.South, PassageDirection.West, false)]

        [TestCase(PassageDirection.West, PassageDirection.North, false)]
        [TestCase(PassageDirection.West, PassageDirection.East, true)]
        [TestCase(PassageDirection.West, PassageDirection.South, false)]
        [TestCase(PassageDirection.West, PassageDirection.West, false)]
        public void Directions_Connect_Correctly(PassageDirection first, PassageDirection second, bool excpected)
        {
            // Given

            var p1 = new Passage(first, PassageIndex.First);
            var p2 = new Passage(second, PassageIndex.First);

            // When

            var actual = GenerationUtility.CanConnect(p1, p2);

            // Then

            Assert.That(actual, Is.EqualTo(excpected));
        }

        [Test]
        public void Passages_With_Unequal_Index_Cannot_Connect()
        {
            // Given

            var p1 = new Passage(PassageDirection.West, PassageIndex.First);
            var p2 = new Passage(PassageDirection.East, PassageIndex.Second);

            // When

            var actual = GenerationUtility.CanConnect(p1, p2);

            // Then

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Passages_With_Equal_Index_Can_Connect()
        {
            // Given

            var p1 = new Passage(PassageDirection.West, PassageIndex.First);
            var p2 = new Passage(PassageDirection.East, PassageIndex.First);

            // When

            var actual = GenerationUtility.CanConnect(p1, p2);

            // Then

            Assert.That(actual, Is.True);
        }

        #endregion

    }

}