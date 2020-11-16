using NUnit.Framework;
using System.Linq;

namespace AChildsCourage.Game.Floors.Building
{

    [TestFixture]
    public class FloorBuildingSessionTests
    {

        #region Tests

        [Test]
        public void Walls_Are_Correctly_Placed_Around_Ground()
        {
            // Given

            var buildingSession = new FloorBuildingSession();
            buildingSession.PlaceGround(new GroundTile(0, 0, 1, 0));

            // When

            buildingSession.GenerateWalls();

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

            Assert.That(buildingSession.WallCount, Is.EqualTo(expectedWallTilePositions.Length), "Incorrect number of walls placed!");

            var actualWallPositions = buildingSession.Walls.Select(w => w.Position).ToArray();
            foreach (var expectedWallPosition in expectedWallTilePositions)
                Assert.That(actualWallPositions.Contains(expectedWallPosition), $"Wall position {expectedWallPosition} not found!");
        }

        [Test]
        public void Occupied_Positions_Are_Removed()
        {
            // Given

            var buildingSession = new FloorBuildingSession();
            buildingSession.PlaceGround(new GroundTile(0, 0, 1, 0));
            buildingSession.PlaceGround(new GroundTile(1, 0, 1, 0));

            // When

            buildingSession.GenerateWalls();

            // Then

            Assert.That(buildingSession.WallCount, Is.EqualTo(18), "Incorrect number of walls!");
        }

        [Test]
        public void Correct_Walls_Are_Detected_As_Side()
        {
            // Given

            var buildingSession = new FloorBuildingSession();
            buildingSession.PlaceGround(new GroundTile(0, 0, 1, 0));

            // When

            buildingSession.GenerateWalls();

            // Then

            var expectedSidePositions = new[]
            {
                new TilePosition(0, 1),
                new TilePosition(0, 2)
            };


            var actualSidePositions = buildingSession.Walls.Where(w => w.Type == WallType.Side).Select(w => w.Position).ToArray();

            Assert.That(actualSidePositions.Length, Is.EqualTo(2), "Incorrect number of positions found!");

            foreach (var expectedWallPosition in expectedSidePositions)
                Assert.That(actualSidePositions.Contains(expectedWallPosition), $"Wall position {expectedWallPosition} not found!");
        }

        [Test]
        public void Correct_Walls_Are_Detected_As_Top()
        {
            // Given

            var buildingSession = new FloorBuildingSession();
            buildingSession.PlaceGround(new GroundTile(0, 0, 1, 0));

            // When

            buildingSession.GenerateWalls();

            // Then

            var expectedTopPositions = new[]
           {
                new TilePosition(-1, -1),
                new TilePosition(0, -1),
                new TilePosition(1, -1),
                new TilePosition(-1, 0),
                new TilePosition(1, 0),
                new TilePosition(-1, 1),
                new TilePosition(1, 1),
                new TilePosition(-1, 2),
                new TilePosition(1, 2),
                new TilePosition(-1, 3),
                new TilePosition(0, 3),
                new TilePosition(1, 3)
            };

            var actualTopPositions = buildingSession.Walls.Where(w => w.Type == WallType.Top).Select(w => w.Position).ToArray();

            Assert.That(actualTopPositions.Length, Is.EqualTo(12), "Incorrect number of positions found!");

            foreach (var expectedWallPosition in expectedTopPositions)
                Assert.That(actualTopPositions.Contains(expectedWallPosition), $"Wall position {expectedWallPosition} not found!");
        }

        #endregion

    }

}