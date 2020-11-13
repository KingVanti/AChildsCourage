using NUnit.Framework;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class FloorTests
    {

        #region Tests

        [Test]
        public void Walls_Are_Correctly_Placed_Around_Ground()
        {
            // Given

            var floor = new Floor();
            floor.PlaceGround(new GroundTile(0, 0, 1, 0), new TileOffset(0, 0));

            // When

            floor.GenerateWalls();

            // Then

            var expectedWallTilePositions = new[]
            {
                new TilePosition(-1, -1),
                new TilePosition(0, -1),
                new TilePosition(1, -1),
                new TilePosition(-1, 0),
                new TilePosition(1, 0),
                new TilePosition(-1, 1),
                new TilePosition(0, 1),
                new TilePosition(1, 1),
                new TilePosition(-1, 2),
                new TilePosition(0, 2),
                new TilePosition(1, 2),
                new TilePosition(-1, 3),
                new TilePosition(0, 3),
                new TilePosition(1, 3)
            };

            Assert.That(floor.WallTileCount, Is.EqualTo(expectedWallTilePositions.Length), "Incorrect number of walls placed!");

            foreach (var expectedWallPosition in expectedWallTilePositions)
                Assert.That(floor.WallTilePositions.Contains(expectedWallPosition), $"Wall position {expectedWallPosition} not found!");
        }

        [Test]
        public void Occupied_Positions_Are_Removed()
        {
            // Given

            var floor = new Floor();
            floor.PlaceGround(new GroundTile(0, 0, 1, 0), new TileOffset(0, 0));
            floor.PlaceGround(new GroundTile(1, 0, 1, 0), new TileOffset(0, 0));

            // When

            floor.GenerateWalls();

            // Then

            Assert.That(floor.WallTileCount, Is.EqualTo(18), "Incorrect number of walls!");
        }

        #endregion

    }

}