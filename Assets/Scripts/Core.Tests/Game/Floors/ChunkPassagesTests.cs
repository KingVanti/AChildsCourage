using NUnit.Framework;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class ChunkPassagesTests
    {

        #region Tests

        [Test]
        public void XMirroring_Switches_East_And_West()
        {
            // Given

            var passages = new ChunkPassages(false, true, false, false);

            // When

            var mirrored = passages.XMirrored;

            // Then

            Assert.That(mirrored.HasEast, Is.EqualTo(passages.HasWest), "East not mirrored correctly!");
            Assert.That(mirrored.HasWest, Is.EqualTo(passages.HasEast), "West not mirrored correctly!");
        }

        [Test]
        public void XMirroring_Has_No_Effect_On_North_And_South()
        {
            // Given

            var passages = new ChunkPassages(true, false, false, false);

            // When

            var mirrored = passages.XMirrored;

            // Then

            Assert.That(mirrored.HasNorth, Is.EqualTo(passages.HasNorth), "North was effected!");
            Assert.That(mirrored.HasSouth, Is.EqualTo(passages.HasSouth), "South was effected!");
        }


        [Test]
        public void Rotating_Moves_Each_Passage_Clockwise_90_Degrees()
        {
            // Given

            var passage = new ChunkPassages(true, false, true, false);

            // When

            var rotated = passage.Rotated;

            // Then

            Assert.That(rotated.HasNorth, Is.EqualTo(passage.HasWest), "West incorrectly rotated!");
            Assert.That(rotated.HasEast, Is.EqualTo(passage.HasNorth), "North incorrectly rotated!");
            Assert.That(rotated.HasSouth, Is.EqualTo(passage.HasEast), "East incorrectly rotated!");
            Assert.That(rotated.HasWest, Is.EqualTo(passage.HasSouth), "South incorrectly rotated!");
        }


        [Test]
        public void Count_Is_Calculated_Correctly()
        {
            // Given

            var passage = new ChunkPassages(true, false, false, true);

            // When

            var count = passage.Count;

            // Then

            Assert.That(count, Is.EqualTo(2), "Count incorrectly calculated!");
        }


        [Test]
        public void Has_Returns_True_For_Passages_That_Exist_In_The_Chunk()
        {
            // Given

            var passages = new ChunkPassages(true, false, false, false);

            // When

            var has = passages.Has(PassageDirection.North);

            // Then

            Assert.That(has, Is.True, "Passage has north!");
        }

        [Test]
        public void Has_Returns_False_For_Passages_That_Dont_Exist_In_The_Chunk()
        {
            // Given

            var passages = new ChunkPassages(true, false, false, false);

            // When

            var has = passages.Has(PassageDirection.East);

            // Then

            Assert.That(has, Is.False, "Passage does not have east!");
        }

        #endregion

    }

}