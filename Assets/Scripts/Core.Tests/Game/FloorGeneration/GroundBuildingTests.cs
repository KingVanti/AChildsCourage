using AChildsCourage.Game.Floors.RoomPersistence;
using NUnit.Framework;
using static AChildsCourage.Game.FloorGenerating;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class GroundBuildingTests
    {
/*
        [Test]
        public void Building_Transforms_All_Tiles_And_Places_Them()
        {
            // Given

            var floor = new FloorInProgress();
            var tiles = new[] { new GroundTileData(new TilePosition(0, 0), 0, 0), new GroundTileData(new TilePosition(1, 0), 0, 0) };
            TransformTile transformer = pos => new TilePosition(pos.X, 1);

            // When

            BuildGround(transformer, tiles, floor);

            // Then

            var expected = new[] { new TilePosition(0, 1), new TilePosition(1, 1) };
            Assert.That(floor.GroundPositions, Is.EqualTo(expected), "Tiles incorrectly built!");
        }


        [Test]
        public void Transforming_A_Ground_Tile_Changes_Its_Position()
        {
            // Given

            var tile = new GroundTileData();
            TransformTile transformer = position => new TilePosition(1, 1);

            // When

            var transformed = TransformGroundTile(tile, transformer);

            // Then

            Assert.That(transformed.Position, Is.EqualTo(new TilePosition(1, 1)), "Position incorrectly transformed!");
        }


        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Changes_Its_Position()
        {
            // Given

            var tile = new GroundTileData();

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.Position, Is.EqualTo(new TilePosition(1, 1)), "Position should change!");
        }

        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Does_Not_Change_Its_Other_Properties()
        {
            // Given

            var tile = new GroundTileData();

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When
        }


        [Test]
        public void Ground_Positions_Are_Placed_Into_Correct_List()
        {
            // Given

            var floor = new FloorInProgress();

            // When

            PlaceGroundTile(new GroundTileData(), floor);

            // Then

            Assert.That(floor.GroundPositions.Contains(new TilePosition(0, 0)), Is.True, "Should be added to ground list!");
        }
*/
    }

}