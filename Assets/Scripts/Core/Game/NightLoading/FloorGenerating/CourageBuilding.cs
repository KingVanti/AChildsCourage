using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using System;
using System.Linq;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorGenerating
    {

        internal static FloorInProgress BuildCourage(TileTransformer transformer, CouragePickupData[] pickups, FloorInProgress floor)
        {
            Func<CouragePickupData, CouragePickupData> transformed = pickup => TransformCouragePickup(pickup, transformer);
            Action<CouragePickupData> place = pickup => PlacePickup(pickup, floor);

            Take(pickups)
            .Select(transformed)
            .ForEach(place);

            return floor;
        }

        internal static CouragePickupData TransformCouragePickup(CouragePickupData pickup, TileTransformer transformer)
        {
            var newPosition = transformer(pickup.Position);

            return pickup.With(newPosition);
        }

        internal static CouragePickupData With(this CouragePickupData pickup, TilePosition position)
        {
            return new CouragePickupData(position, pickup.Variant);
        }

        internal static void PlacePickup(CouragePickupData pickup, FloorInProgress floor)
        {
            switch (pickup.Variant)
            {
                case CourageVariant.Orb:
                    floor.CourageOrbPositions.Add(pickup.Position);
                    break;
                case CourageVariant.Spark:
                    floor.CourageSparkPositions.Add(pickup.Position);
                    break;
            }
        }

    }

}