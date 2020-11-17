using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.Floors.FloorTilesBuilding;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class TileTransformationTests
    {

        #region Tests

        [Test]
        public void Transforming_A_Ground_Tile_Offsets_It()
        {
            // Given

            var transformer = new TilePositionTransformer(new TileOffset(1, -1));

            // When

            var transformed = transformer.Transform(new GroundTile(0, 0, 0, 0));

            // When

            Assert.That(transformed.Position, Is.EqualTo(new TilePosition(1, -1)));
        }

        #endregion

    }

}