﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.Courage;
using static AChildsCourage.Game.MOldFloorGenerating.MFloorBuilder;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.F;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MOldFloorGenerating
    {

        public static class MCouragePickupFiltering
        {

            private const int CourageOrbCount = 5;
            private const int CourageSparkCount = 25;


            internal static IEnumerable<CouragePickup> ChooseCouragePickups(FloorBuilder builder, CreateRng rng) =>
                ChoosePickupsOfVariant(builder, CourageVariant.Spark, CourageSparkCount, CalculateCourageSparkWeight, rng)
                    .Concat(ChoosePickupsOfVariant(builder, CourageVariant.Orb, CourageOrbCount, CalculateCourageOrbWeight, rng));

            private static IEnumerable<CouragePickup> ChoosePickupsOfVariant(FloorBuilder floorBuilder, CourageVariant variant, int count, CouragePickupWeightFunction weightFunction, CreateRng rng)
            {
                var positions = GetCouragePositionsOfVariant(floorBuilder, variant).ToImmutableHashSet();

                ImmutableHashSet<TilePosition> AddNext(ImmutableHashSet<TilePosition> taken) => taken.Add(ChooseNextPickupPosition(positions, taken, weightFunction, rng));

                return AggregateTimes(ImmutableHashSet<TilePosition>.Empty, AddNext, count)
                    .Select(p => new CouragePickup(p, variant));
            }

            private static IEnumerable<TilePosition> GetCouragePositionsOfVariant(FloorBuilder floorBuilder, CourageVariant variant) =>
                floorBuilder.Rooms
                            .SelectMany(r => r.CouragePickups)
                            .Where(p => p.Variant == variant)
                            .Select(p => p.Position);

            private static TilePosition ChooseNextPickupPosition(IEnumerable<TilePosition> positions, ImmutableHashSet<TilePosition> taken, CouragePickupWeightFunction weightFunction, CreateRng rng)
            {
                bool IsNotTaken(TilePosition p) => !taken.Contains(p);

                float CalculateWeight(TilePosition p) => weightFunction(p, taken);

                return Take(positions)
                       .Where(IsNotTaken)
                       .GetWeightedRandom(CalculateWeight, rng);
            }

            private static float CalculateCourageOrbWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
            {
                var distanceOriginWeight =
                    position
                        .Map(GetDistanceFromOrigin)
                        .Clamp(20, 40)
                        .Remap(20f, 40f, 1, 10f);

                var distanceToClosestWeight = taken.Any()
                    ? taken.Select(p => p.Map(DistanceTo, position)).Min()
                           .Clamp(10, 30)
                           .Remap(10, 30, 1, 20)
                    : 20;

                return distanceOriginWeight + distanceToClosestWeight;
            }

            private static float CalculateCourageSparkWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
            {
                var distanceToClosestWeight = taken.Any()
                    ? taken.Select(p => p.Map(DistanceTo, position)).Min()
                           .Clamp(1, 10)
                           .Remap(1, 10, 4, 1)
                    : 1;

                return distanceToClosestWeight;
            }


            private delegate float CouragePickupWeightFunction(TilePosition position, ImmutableHashSet<TilePosition> taken);

        }

    }

}