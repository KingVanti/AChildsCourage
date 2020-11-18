using NUnit.Framework;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class TransformerCreatingTests
    {

        #region Tests

        [Test]
        public void Center_For_Chunks_Are_Calculated_Correctly()
        {
            // Given

            var chunkPosition = new ChunkPosition(1, -1);

            // When

            var center = chunkPosition.GetChunkCenter();

            // Then

            Assert.That(center, Is.EqualTo(new TilePosition(61, -21)), "Center calculated incorrectly!");
        }

        #endregion

    }

}