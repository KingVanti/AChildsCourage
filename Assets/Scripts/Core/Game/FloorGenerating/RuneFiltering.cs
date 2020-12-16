using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MFloorGenerating.MFloorBuilder;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.F;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MRuneFiltering
        {

            private const int RuneCount = 5;


            public static Func<FloorBuilder, CreateRng, IEnumerable<Rune>> ChooseRunes =>
                (floorBuilder, rng) =>
                {
                    var runePositions = floorBuilder
                                        .Rooms
                                        .SelectMany(r => r.Runes)
                                        .Select(r => r.Position)
                                        .ToImmutableHashSet();

                    ImmutableHashSet<TilePosition> AddNext(ImmutableHashSet<TilePosition> taken) => taken.Add(ChooseNextRunePosition(runePositions, taken, rng));

                    return AggregateTimes(ImmutableHashSet<TilePosition>.Empty, AddNext, RuneCount)
                        .Select(p => new Rune(p));
                };


            private static TilePosition ChooseNextRunePosition(IEnumerable<TilePosition> positions, ImmutableHashSet<TilePosition> taken, CreateRng rng)
            {
                bool IsNotTaken(TilePosition p) => !taken.Contains(p);

                float CalculateWeight(TilePosition p) => CalculateRuneWeight(p, taken);

                return Take(positions)
                       .Where(IsNotTaken)
                       .GetWeightedRandom(CalculateWeight, rng);
            }

            private static float CalculateRuneWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
            {
                var distanceToClosestWeight = taken.Any() ?
                    taken.Select(p => GetDistanceBetween(position, p)).Min()
                         .Clamp(25, 60)
                         .Remap(25, 60, 1, 20) :
                    20;

                return distanceToClosestWeight;
            }

        }

    }

}