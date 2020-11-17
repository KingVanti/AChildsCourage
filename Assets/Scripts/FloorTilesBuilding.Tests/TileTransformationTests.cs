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
        public void Transforming_A_Position_Offsets_It()
        {
            // Given

            var transformer = new TilePositionTransformer(new TileOffset(1, -1));

            // When

            var transformed = transformer.Transform(new TilePosition(0, 0));

            // When

            Assert.That(transformed, Is.EqualTo(new TilePosition(1, -1)));
        }

        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Changes_Its_Position()
        {
            // Given

            var tile = new GroundTile(0, 0, 0, 0);

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.Position, Is.EqualTo(new TilePosition(1, 1)), "Position should change!");
        }

        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Does_Not_Change_Its_Other_Properties()
        {
            // Given

            var tile = new GroundTile(0, 0, 0, 0);

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.DistanceToWall, Is.EqualTo(tile.DistanceToWall), "Distance to wall should not change!");
            Assert.That(newtile.AOIIndex, Is.EqualTo(tile.AOIIndex), "AOI index should not change!");
        }

        #endregion

    }

}