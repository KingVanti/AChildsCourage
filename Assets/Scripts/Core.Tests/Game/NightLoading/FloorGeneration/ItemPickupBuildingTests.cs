using AChildsCourage.Game.Floors.RoomPersistance;
using NUnit.Framework;
using static AChildsCourage.Game.NightLoading.FloorGenerating;

namespace AChildsCourage.Game.NightLoading
{

    [TestFixture]
    public class ItemPickupBuildingTests
    {

        [Test]
        public void Building_Transforms_All_Pickups_And_Places_Them()
        {
            // Given

            var floor = new FloorInProgress();
            var pickups = new[] { new ItemPickupData(new TilePosition(0, 0)), new ItemPickupData(new TilePosition(1, 0)) };
            TileTransformer transformer = pos => new TilePosition(pos.X, 1);

            // When

            BuildItemPickups(transformer, pickups, floor);

            // Then

            var expected = new[] { new TilePosition(0, 1), new TilePosition(1, 1) };
            Assert.That(floor.ItemPickupPositions, Is.EqualTo(expected), "Tiles incorrectly built!");
        }


        [Test]
        public void Transforming_A_Pickup_Changes_Its_Position()
        {
            // Given

            var pickup = new ItemPickupData(new TilePosition(0, 0));
            TileTransformer transformer = position => new TilePosition(1, 1);

            // When

            var transformed = TransformItemPickup(pickup, transformer);

            // Then

            Assert.That(transformed.Position, Is.EqualTo(new TilePosition(1, 1)), "Position incorrectly transformed!");
        }


        [Test]
        public void Creating_A_Pickup_With_A_New_Position_Changes_Its_Position()
        {
            // Given

            var pickup = new ItemPickupData(new TilePosition(0, 0));

            // When

            var newPickup = pickup.With(new TilePosition(1, 1));

            // When

            Assert.That(newPickup.Position, Is.EqualTo(new TilePosition(1, 1)), "Position should change!");
        }


        [Test]
        public void Pickups_Are_Placed_Into_Correct_List()
        {
            // Given

            var floor = new FloorInProgress();

            // When

            PlaceItemPickup(new ItemPickupData(new TilePosition(0, 0)), floor);

            // Then

            Assert.That(floor.ItemPickupPositions.Contains(new TilePosition(0, 0)), Is.True, "Should be added to item-pickup list!");
        }

    }

}