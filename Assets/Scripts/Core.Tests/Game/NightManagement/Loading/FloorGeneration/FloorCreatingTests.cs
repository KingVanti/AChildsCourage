using AChildsCourage.Game.Floors;
using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.NightManagement.Loading.FloorCreating;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class FloorCreatingTests
    {

        #region Tests

        [Test]
        public void Creating_A_Floor_Copies_Ground_Positions()
        {
            // Given

            var positions = new[]
            {
                new TilePosition(0, 0),
                new TilePosition(1, 0)
            };
            var floorInProgress = new FloorInProgress();
            positions.ForEach(floorInProgress.GroundPositions.Add);

            WallGenerator wallGenerator = _ => Enumerable.Empty<Wall>();
            CouragePositionChooser couragePositionChooser = _ => Enumerable.Empty<TilePosition>();

            // When

            var floor = CreateFloor(floorInProgress, wallGenerator, couragePositionChooser);

            // Then

            Assert.That(floor.GroundTiles.Select(t => t.Position), Is.EqualTo(positions), "Positions incorrectly copied!");
        }

        [Test]
        public void Creating_A_Floor_Creates_And_Stores_Walls()
        {
            // Given

            var positions = new[]
            {
                new TilePosition(0, 0),
                new TilePosition(1, 0)
            };
            var floorInProgress = new FloorInProgress();
            positions.Select(floorInProgress.WallPositions.Add);

            var expected = new[]
            {
                new Wall(new TilePosition(0, 0), WallType.Side),
                new Wall(new TilePosition(1, 0), WallType.Side)
            };
            WallGenerator wallGenerator = _ => expected;
            CouragePositionChooser couragePositionChooser = _ => Enumerable.Empty<TilePosition>();

            // When

            var floor = CreateFloor(floorInProgress, wallGenerator, couragePositionChooser);

            // Then

            Assert.That(floor.Walls, Is.EqualTo(expected), "Walls incorrectly copied!");
        }

        [Test]
        public void Creating_A_Floor_Chooses_And_Stored_Courage_Orb_Positions()
        {
            // Given

            var positions = new[]
            {
                new TilePosition(0, 0),
                new TilePosition(1, 0)
            };
            var floorInProgress = new FloorInProgress();
            positions.Select(floorInProgress.CourageOrbPositions.Add);

            WallGenerator wallGenerator = _ => Enumerable.Empty<Wall>();
            CouragePositionChooser couragePositionChooser = _ => positions;

            // When

            var floor = CreateFloor(floorInProgress, wallGenerator, couragePositionChooser);

            // Then

            Assert.That(floor.CourageOrbPositions, Is.EqualTo(positions), "Positions incorrectly copied!");
        }

        #endregion

    }

}