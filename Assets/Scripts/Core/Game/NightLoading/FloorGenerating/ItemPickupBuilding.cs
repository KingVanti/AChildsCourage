using System;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.F;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorGenerating
    {

        internal static FloorInProgress BuildItemPickups(TransformTile transformer, ItemPickupData[] pickups, FloorInProgress floor)
        {
            Func<ItemPickupData, ItemPickupData> transformed = pickup => TransformItemPickup(pickup, transformer);
            Action<ItemPickupData> place = pickup => PlaceItemPickup(pickup, floor);

            Take(pickups)
                .Select(transformed)
                .ForEach(place);

            return floor;
        }

        internal static ItemPickupData TransformItemPickup(ItemPickupData pickup, TransformTile transformer)
        {
            var newPosition = transformer(pickup.Position);

            return pickup.With(newPosition);
        }

        internal static ItemPickupData With(this ItemPickupData pickup, TilePosition position)
        {
            return new ItemPickupData(position);
        }

        internal static void PlaceItemPickup(ItemPickupData pickup, FloorInProgress floor)
        {
            floor.ItemPickupPositions.Add(pickup.Position);
        }

    }

}