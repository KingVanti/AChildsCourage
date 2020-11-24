using AChildsCourage.Game.Floors;
using System;
using System.Collections.Generic;
using System.Linq;

using static AChildsCourage.Game.NightLoading.FloorGeneratingUtility;

namespace AChildsCourage.Game.NightLoading
{

    internal static class ItemPickupCreating
    {

        internal static ItemPickupCreator GetDefault(ItemIdLoader loadItemIds)
        {
            return floor =>
            {
                var itemIds = loadItemIds();

                return ChoosePickups(itemIds, floor.ItemPickupPositions);
            };
        }


        internal static IEnumerable<ItemPickup> ChoosePickups(IEnumerable<int> itemIds, IEnumerable<TilePosition> itemPositions)
        {
            var remainingPositions = itemPositions.ToList();
            var takenPositions = new List<TilePosition>();

            foreach (var itemId in itemIds)
            {
                var nextPosition = ChooseNextPosition(remainingPositions, takenPositions);

                remainingPositions.Remove(nextPosition);
                takenPositions.Add(nextPosition);

                yield return new ItemPickup(nextPosition, itemId);
            }
        }

        internal static TilePosition ChooseNextPosition(IEnumerable<TilePosition> positions, IEnumerable<TilePosition> taken)
        {
            Func<TilePosition, float> weight = p => CalculatePositionWeight(p, taken);

            return
                positions
                .OrderByDescending(weight)
                .First();
        }

        internal static float CalculatePositionWeight(TilePosition position, IEnumerable<TilePosition> taken)
        {
            var distanceToOrigin = GetDistanceFromOrigin(position);
            var distanceToOther = taken.Count() > 0 ? taken.Min(p => GetDistanceBetween(position, p)) : 0;

            var distanceToOriginWeight = distanceToOrigin != 0 ? 1f / distanceToOrigin : 1.1f;
            var distanceToOtherWeight = distanceToOther;

            return distanceToOriginWeight + distanceToOtherWeight;
        }

    }

}