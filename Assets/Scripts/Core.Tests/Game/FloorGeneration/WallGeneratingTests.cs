using System;
using System.Linq;
using AChildsCourage.Game.Floors;
using NUnit.Framework;
using static AChildsCourage.Game.FloorGenerating;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class WallGeneratingTests
    {
/*
        [Test]
        public void Wall_Is_Above_Ground_If_There_Is_Floor_One_Tile_Below()
        {
            // Given

            Func<TilePosition, bool> hasGroundAt = pos => pos.Equals(new TilePosition(0, -1));

            // When

            var hasGroundBelow = HasGroundBelow(new TilePosition(0, 0), hasGroundAt);

            // Then

            Assert.That(hasGroundBelow, Is.True, "Should have ground below!");
        }

        [Test]
        public void Wall_Is_Above_Ground_If_There_Is_Floor_Two_Tiles_Below()
        {
            // Given

            Func<TilePosition, bool> hasGroundAt = pos => pos.Equals(new TilePosition(0, -2));

            // When

            var hasGroundBelow = HasGroundBelow(new TilePosition(0, 0), hasGroundAt);

            // Then

            Assert.That(hasGroundBelow, Is.True, "Should have ground below!");
        }

        [Test]
        public void Wall_Is_Not_Above_Ground_If_There_Is_No_Floor_One_Or_Two_Tiles_Below()
        {
            // Given

            Func<TilePosition, bool> hasGroundAt = pos => pos.Equals(new TilePosition(0, -3));

            // When

            var hasGroundBelow = HasGroundBelow(new TilePosition(0, 0), hasGroundAt);

            // Then

            Assert.That(hasGroundBelow, Is.False, "Should have no ground below!");
        }


        [Test]
        public void Top_Walls_Are_Correctly_Created()
        {
            // Given

            var position = new TilePosition(0, 0);
            var hasGroundBelow = false;

            // When

            var wall = CreateWall(position, hasGroundBelow);

            // Then

            Assert.That(wall.Position, Is.EqualTo(position), "Position incorrectly copied!");
            Assert.That(wall.Type, Is.EqualTo(WallType.Top), "Type incorrectly set!");
        }

        [Test]
        public void Side_Walls_Are_Correctly_Created()
        {
            // Given

            var position = new TilePosition(0, 0);
            var hasGroundBelow = true;

            // When

            var wall = CreateWall(position, hasGroundBelow);

            // Then

            Assert.That(wall.Position, Is.EqualTo(position), "Position incorrectly copied!");
            Assert.That(wall.Type, Is.EqualTo(WallType.Side), "Type incorrectly set!");
        }


        [Test]
        public void Can_Get_Correct_Positions_For_Ground_Checking()
        {
            // Given

            var wallPosition = new TilePosition(0, 0);

            // When

            var positions = GetCheckGroundPositions(wallPosition);

            // Then

            var expected = new[] { new TilePosition(0, -1), new TilePosition(0, -2) };

            Assert.That(positions.Count(), Is.EqualTo(expected.Length), "Found incorrect number of positions!");

            foreach (var position in positions)
                Assert.That(expected.Contains(position), "Found unexpected position!");
        }


        [Test]
        public void Ground_Offsets_Are_Calculated_Based_On_Wall_Height()
        {
            // When

            var offsets = GetGroundOffsets();

            // Then

            var expected = new[] { new TileOffset(0, -1), new TileOffset(0, -2) };

            Assert.That(offsets.Count(), Is.EqualTo(expected.Length), "Found incorrect number of offsets!");

            foreach (var offset in offsets)
                Assert.That(expected.Contains(offset), "Found unexpected offset!");
        }


        [Test]
        public void A_Builder_Has_Floor_At_A_Position_When_Floor_Was_Placed_There()
        {
            // Given

            var floor = new FloorInProgress();
            floor.GroundPositions.Add(new TilePosition(0, 0));

            // When

            var hasGround = floor.HasGroundAt(new TilePosition(0, 0));

            // Then

            Assert.That(hasGround, Is.True, "Should find ground!");
        }

        [Test]
        public void A_Builder_Has_No_Floor_At_A_Position_When_No_Floor_Was_Placed_There()
        {
            // Given

            var floor = new FloorInProgress();

            // When

            var hasGround = floor.HasGroundAt(new TilePosition(0, 0));

            // Then

            Assert.That(hasGround, Is.False, "Should not find ground!");
        }
*/
    }

}