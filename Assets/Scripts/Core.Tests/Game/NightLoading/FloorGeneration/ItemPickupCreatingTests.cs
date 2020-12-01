using System.Linq;
using AChildsCourage.Game.Items;
using NUnit.Framework;
using static AChildsCourage.Game.NightLoading.FloorGenerating;

namespace AChildsCourage.Game.NightLoading
{

    [TestFixture]
    public class ItemPickupCreatingTests
    {

        [Test]
        public void Each_Item_Is_Chosen_Exactly_Once()
        {
            // Given

            var itemIds = new[] { (ItemId) 0, (ItemId) 1 };
            var positions = new[] { new MTilePosition.TilePosition(0, 0), new MTilePosition.TilePosition(1, 0), new MTilePosition.TilePosition(2, 0) };

            // When

            var items = ChoosePickups(itemIds, positions);

            // Then

            Assert.That(items.Select(i => i.ItemId), Is.EqualTo(itemIds), "Did not choose correct item ids!");
        }

        [Test]
        public void Item_Positions_Are_Chosen_According_To_Weight()
        {
            // Given

            var itemIds = new[] { (ItemId) 0, (ItemId) 1 };
            var positions = new[] { new MTilePosition.TilePosition(0, 0), new MTilePosition.TilePosition(1, 0), new MTilePosition.TilePosition(2, 0) };

            // When

            var items = ChoosePickups(itemIds, positions);

            // Then

            var expectedPositions = new[] { new MTilePosition.TilePosition(0, 0), new MTilePosition.TilePosition(2, 0) };
            Assert.That(items.Select(i => i.Position), Is.EqualTo(expectedPositions), "Did not choose correct item positions!");
        }


        [Test]
        public void Position_Weight_Is_Inversly_Proportional_To_Distance_To_Origin()
        {
            // Given

            var position1 = new MTilePosition.TilePosition(1, 0);
            var position2 = new MTilePosition.TilePosition(2, 0);

            // When

            var weight1 = CalculatePositionWeight(position1, Enumerable.Empty<MTilePosition.TilePosition>());
            var weight2 = CalculatePositionWeight(position2, Enumerable.Empty<MTilePosition.TilePosition>());

            // Then

            Assert.That(weight1, Is.GreaterThan(weight2), "Weight 1 should be greater than weight 2!");
        }

        [Test]
        public void Position_Weight_Is_Proportional_To_Min_Distance_To_Taken_Positions()
        {
            // Given

            var position1 = new MTilePosition.TilePosition(1, 0);
            var position2 = new MTilePosition.TilePosition(1, 0);

            // When

            var weight1 = CalculatePositionWeight(position1, new[] { new MTilePosition.TilePosition(3, 0) });
            var weight2 = CalculatePositionWeight(position2, new[] { new MTilePosition.TilePosition(2, 0) });

            // Then

            Assert.That(weight1, Is.GreaterThan(weight2), "Weight 1 should be greater than weight 2!");
        }

    }

}