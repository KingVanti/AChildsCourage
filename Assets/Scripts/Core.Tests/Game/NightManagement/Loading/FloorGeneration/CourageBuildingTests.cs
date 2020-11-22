using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using NUnit.Framework;
using static AChildsCourage.Game.NightManagement.Loading.CourageBuilding;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class CourageBuildingTests
    {

        #region Tests

        [Test]
        public void Building_Transforms_All_Pickups_And_Places_Them()
        {
            // Given

            var floor = new FloorInProgress();
            var pickups = new[]
            {
                new CouragePickupData(new TilePosition(0, 0), CourageVariant.Orb),
                new CouragePickupData(new TilePosition(1, 0), CourageVariant.Orb)
            };
            TileTransformer transformer = pos => new TilePosition(pos.X, 1);

            // When

            BuildCourage(floor, pickups, transformer);

            // Then

            var expected = new[]
            {
                new TilePosition(0, 1),
                new TilePosition(1, 1)
            };
            Assert.That(floor.CourageOrbPositions, Is.EqualTo(expected), "Tiles incorrectly built!");
        }


        [Test]
        public void Transforming_A_Pickup_Changes_Its_Position()
        {
            // Given

            var pickup = new CouragePickupData(new TilePosition(0, 0), CourageVariant.Orb);
            TileTransformer transformer = position => new TilePosition(1, 1);

            // When

            var transformed = TransformCouragePickup(pickup, transformer);

            // Then

            Assert.That(transformed.Position, Is.EqualTo(new TilePosition(1, 1)), "Position incorrectly transformed!");
        }


        [Test]
        public void Creating_A_Pickup_With_A_New_Position_Changes_Its_Position()
        {
            // Given

            var pickup = new CouragePickupData(new TilePosition(0, 0), CourageVariant.Orb);

            // When

            var newtile = pickup.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.Position, Is.EqualTo(new TilePosition(1, 1)), "Position should change!");
        }

        [Test]
        public void Creating_A_Pickup_With_A_New_Position_Does_Not_Change_Its_Other_Properties()
        {
            // Given

            var pickup = new CouragePickupData(new TilePosition(0, 0), CourageVariant.Orb);

            // When

            var newtile = pickup.With(new TilePosition(1, 1));

            // When


        }


        [Test]
        public void Courage_Orbs_Are_Placed_Into_Correct_List()
        {
            // Given

            var floor = new FloorInProgress();

            // When

            PlacePickup(new CouragePickupData(new TilePosition(0, 0), CourageVariant.Orb), floor);

            // Then

            Assert.That(floor.CourageSparkPositions.Contains(new TilePosition(0, 0)), Is.False, "Should not be added to spark list!");
            Assert.That(floor.CourageOrbPositions.Contains(new TilePosition(0, 0)), Is.True, "Should be added to orb list!");
        }

        [Test]
        public void Courage_Spark_Are_Placed_Into_Correct_List()
        {
            // Given

            var floor = new FloorInProgress();

            // When

            PlacePickup(new CouragePickupData(new TilePosition(0, 0), CourageVariant.Spark), floor);

            // Then

            Assert.That(floor.CourageSparkPositions.Contains(new TilePosition(0, 0)), Is.True, "Should be added to spark list!");
            Assert.That(floor.CourageOrbPositions.Contains(new TilePosition(0, 0)), Is.False, "Should not be added to orb list!");
        }

        #endregion

    }

}