using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Items;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorGenerating
    {

        internal static IEnumerable<ItemPickup> ChoosePickups(IEnumerable<ItemId> itemIds, IEnumerable<TilePosition> itemPositions)
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
            var distanceToOrigin = TilePosition.GetDistanceFromOrigin(position);
            var positions = taken as TilePosition[] ?? taken.ToArray();
            var distanceToOther = positions.Any() ? positions.Min(p => TilePosition.GetDistanceBetween(position, p)) : 0;

            var distanceToOriginWeight = distanceToOrigin != 0 ? 1f / distanceToOrigin : 1.1f;
            var distanceToOtherWeight = distanceToOther;

            return distanceToOriginWeight + distanceToOtherWeight;
        }

    }

}