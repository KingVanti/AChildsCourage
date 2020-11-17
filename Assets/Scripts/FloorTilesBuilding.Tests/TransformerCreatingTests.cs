using NUnit.Framework;
using static AChildsCourage.Game.Floors.FloorTilesBuilding;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class TransformerCreatingTests
    {

        #region Tests

        [Test]
        public void Tile_Offset_For_Chunk_Positions_Are_Calculated_Correctly()
        {
            // Given

            var chunkPosition = new ChunkPosition(1, -1);

            // When

            var tileOffset = GetTileOffsetFor(chunkPosition);

            // Then

            Assert.That(tileOffset, Is.EqualTo(new TileOffset(41, -41)), "Tile-offset calculated incorrectly!");
        }

        #endregion

    }

}