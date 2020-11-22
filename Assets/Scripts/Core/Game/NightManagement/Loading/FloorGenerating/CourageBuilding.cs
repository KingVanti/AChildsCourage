using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using System;
using System.Linq;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class CourageBuilding
    {

        internal static CourageBuilder GetDefault()
        {
            return BuildCourage;
        }


        internal static FloorBuilder BuildCourage(FloorBuilder builder, CouragePickupData[] pickups, TileTransformer transformer)
        {
            Func<CouragePickupData, CouragePickupData> transformed = pickup => TransformCouragePickup(pickup, transformer);
            Action<CouragePickupData> place = pickup => PlacePickup(pickup, builder);

            Pipe(pickups)
            .Select(transformed)
            .AllInto(place);

            return builder;
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

        internal static void PlacePickup(CouragePickupData pickup, FloorBuilder builder)
        {
            switch (pickup.Variant)
            {
                case CourageVariant.Orb:
                    builder.CourageOrbPositions.Add(pickup.Position);
                    break;
                case CourageVariant.Spark:
                    builder.CourageSparkPositions.Add(pickup.Position);
                    break;
            }
        }

    }

}