﻿using AChildsCourage.Game.Floors.RoomPersistance;
using NUnit.Framework;
using static AChildsCourage.Game.NightLoading.FloorGenerating;

namespace AChildsCourage.Game.NightLoading
{

    [TestFixture]
    public class GroundBuildingTests
    {

        [Test]
        public void Building_Transforms_All_Tiles_And_Places_Them()
        {
            // Given

            var floor = new FloorInProgress();
            var tiles = new[] { new GroundTileData(new MTilePosition.TilePosition(0, 0), 0, 0), new GroundTileData(new MTilePosition.TilePosition(1, 0), 0, 0) };
            TransformTile transformer = pos => new MTilePosition.TilePosition(pos.X, 1);

            // When

            BuildGroundTiles(transformer, tiles, floor);

            // Then

            var expected = new[] { new MTilePosition.TilePosition(0, 1), new MTilePosition.TilePosition(1, 1) };
            Assert.That(floor.GroundPositions, Is.EqualTo(expected), "Tiles incorrectly built!");
        }


        [Test]
        public void Transforming_A_Ground_Tile_Changes_Its_Position()
        {
            // Given

            var tile = new GroundTileData();
            TransformTile transformer = position => new MTilePosition.TilePosition(1, 1);

            // When

            var transformed = TransformGroundTile(tile, transformer);

            // Then

            Assert.That(transformed.Position, Is.EqualTo(new MTilePosition.TilePosition(1, 1)), "Position incorrectly transformed!");
        }


        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Changes_Its_Position()
        {
            // Given

            var tile = new GroundTileData();

            // When

            var newtile = tile.With(new MTilePosition.TilePosition(1, 1));

            // When

            Assert.That(newtile.Position, Is.EqualTo(new MTilePosition.TilePosition(1, 1)), "Position should change!");
        }

        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Does_Not_Change_Its_Other_Properties()
        {
            // Given

            var tile = new GroundTileData();

            // When

            var newtile = tile.With(new MTilePosition.TilePosition(1, 1));

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

            Assert.That(floor.GroundPositions.Contains(new MTilePosition.TilePosition(0, 0)), Is.True, "Should be added to ground list!");
        }

    }

}