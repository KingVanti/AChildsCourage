using AChildsCourage.Game.Floors.RoomPersistance;
using System;
using System.Linq;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightLoading
{

    internal static class ItemPickupBuilding
    {

        internal static ItemPickupBuilder GetDefault(TileTransformer transformer)
        {
            return (pickups, floor) =>
            {
                return BuildItemPickups(transformer, pickups, floor);
            };
        }


        internal static FloorInProgress BuildItemPickups(TileTransformer transformer, ItemPickupData[] pickups, FloorInProgress floor)
        {
            Func<ItemPickupData, ItemPickupData> transformed = pickup => TransformItemPickup(pickup, transformer);
            Action<ItemPickupData> place = pickup => PlaceItemPickup(pickup, floor);

            Take(pickups)
            .Select(transformed)
            .ForEach(place);

            return floor;
        }

        internal static ItemPickupData TransformItemPickup(ItemPickupData pickup, TileTransformer transformer)
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