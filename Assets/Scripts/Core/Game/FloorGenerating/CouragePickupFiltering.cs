using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.CollectionRNG;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.F;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        internal const int CourageOrbCount = 5;
        internal const int CourageSparkCount = 25;


        internal static IEnumerable<CouragePickup> ChooseCouragePickups(FloorBuilder floorBuilder, Rng.CreateRng rng)
        {
            var sparks = ChoosePickupsOfVariant(floorBuilder, CourageVariant.Spark, CourageSparkCount, CalculateCourageSparkWeight, rng);
            var orbs = ChoosePickupsOfVariant(floorBuilder, CourageVariant.Orb, CourageOrbCount, CalculateCourageOrbWeight, rng);

            return sparks.Concat(orbs);
        }


        private static IEnumerable<CouragePickup> ChoosePickupsOfVariant(FloorBuilder floorBuilder, CourageVariant variant, int count, CouragePickupWeightFunction weightFunction, Rng.CreateRng rng)
        {
            var positions = GetCouragePositionsOfVariant(floorBuilder, variant).ToImmutableHashSet();

            Func<ImmutableHashSet<TilePosition>, ImmutableHashSet<TilePosition>> addNext =
                taken =>
                    taken.Add(ChooseNextPickupPosition(positions, taken, weightFunction, rng));

            return Take(ImmutableHashSet<TilePosition>.Empty)
                   .RepeatFor(addNext, count)
                   .Select(p => new CouragePickup(p, variant));
        }

        private static IEnumerable<TilePosition> GetCouragePositionsOfVariant(FloorBuilder floorBuilder, CourageVariant variant) =>
            floorBuilder.Rooms
                        .SelectMany(r => r.CouragePickups)
                        .Where(p => p.Variant == variant)
                        .Select(p => p.Position);


        internal static TilePosition ChooseNextPickupPosition(IEnumerable<TilePosition> positions, ImmutableHashSet<TilePosition> taken, CouragePickupWeightFunction weightFunction, Rng.CreateRng rng)
        {
            Func<TilePosition, bool> isNotTaken = p => !taken.Contains(p);
            CalculateWeight<TilePosition> calculateWeight = p => weightFunction(p, taken);

            return Take(positions)
                   .Where(isNotTaken)
                   .GetWeightedRandom(calculateWeight, rng);
        }

        internal static float CalculateCourageOrbWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
        {
            var distanceOriginWeight =
                GetDistanceFromOrigin(position)
                    .Clamp(20, 40)
                    .Remap(20f, 40f, 1, 10f);

            var distanceToClosestWeight = taken.Any()
                ? taken.Select(p => GetDistanceBetween(position, p)).Min()
                       .Clamp(10, 30)
                       .Remap(10, 30, 1, 20)
                : 20;

            return distanceOriginWeight + distanceToClosestWeight;
        }

        internal static float CalculateCourageSparkWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
        {
            var distanceToClosestWeight = taken.Any()
                ? taken.Select(p => GetDistanceBetween(position, p)).Min()
                       .Clamp(1, 10)
                       .Remap(1, 10, 4, 1)
                : 1;

            return distanceToClosestWeight;
        }

    }

}