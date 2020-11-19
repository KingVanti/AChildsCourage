using AChildsCourage.Game.Floors;
using NUnit.Framework;
using static AChildsCourage.Game.NightManagement.Loading.DataBuilding;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class DataBuildingTests
    {

        #region Tests

        [Test]
        public void Building_Transforms_All_Tiles_And_Places_Them()
        {
            // Given

            var builder = new FloorBuilder();
            var tiles = new Tiles<DataTile>(new[]
            {
                new DataTile(new TilePosition(0, 0), DataTileType.CourageOrb),
                new DataTile(new TilePosition(1, 0), DataTileType.CourageOrb)
            });
            TileTransformer transformer = pos => new TilePosition(pos.X, 1);

            // When

            BuildDataTiles(builder, tiles, transformer);

            // Then

            var expected = new[]
            {
                new TilePosition(0, 1),
                new TilePosition(1, 1)
            };
            Assert.That(builder.CourageOrbPositions, Is.EqualTo(expected), "Tiles incorrectly built!");
        }


        [Test]
        public void Transforming_A_Data_Tile_Changes_Its_Position()
        {
            // Given

            var tile = new DataTile(new TilePosition(0, 0), DataTileType.CourageOrb);
            TileTransformer transformer = position => new TilePosition(1, 1);

            // When

            var transformed = TransformDataTile(tile, transformer);

            // Then

            Assert.That(transformed.Position, Is.EqualTo(new TilePosition(1, 1)), "Position incorrectly transformed!");
        }


        [Test]
        public void Creating_A_Data_Tile_With_A_New_Position_Changes_Its_Position()
        {
            // Given

            var tile = new DataTile(new TilePosition(0, 0), DataTileType.CourageOrb);

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.Position, Is.EqualTo(new TilePosition(1, 1)), "Position should change!");
        }

        [Test]
        public void Creating_A_Data_Tile_With_A_New_Position_Does_Not_Change_Its_Other_Properties()
        {
            // Given

            var tile = new DataTile(new TilePosition(0, 0), DataTileType.CourageOrb);

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.Type, Is.EqualTo(tile.Type), "Type should not change!");
        }


        [Test]
        public void Courage_Orbs_Are_Placed_Into_Correct_List()
        {
            // Given

            var builder = new FloorBuilder();

            // When

            PlaceDataTile(new DataTile(new TilePosition(0, 0), DataTileType.CourageOrb), builder);

            // Then

            Assert.That(builder.CourageSparkPositions.Contains(new TilePosition(0, 0)), Is.False, "Should not be added to spark list!");
            Assert.That(builder.CourageOrbPositions.Contains(new TilePosition(0, 0)), Is.True, "Should be added to orb list!");
        }

        [Test]
        public void Courage_Spark_Are_Placed_Into_Correct_List()
        {
            // Given

            var builder = new FloorBuilder();

            // When

            PlaceDataTile(new DataTile(new TilePosition(0, 0), DataTileType.CourageSpark), builder);

            // Then

            Assert.That(builder.CourageSparkPositions.Contains(new TilePosition(0, 0)), Is.True, "Should be added to spark list!");
            Assert.That(builder.CourageOrbPositions.Contains(new TilePosition(0, 0)), Is.False, "Should not be added to orb list!");
        }

        #endregion

    }

}