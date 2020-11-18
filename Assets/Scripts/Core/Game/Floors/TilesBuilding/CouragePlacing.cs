using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private const int CourageOrbCount = 5;


        private static IEnumerable<TilePosition> ChooseCourageOrbPositions(this FloorTilesBuilder builder, IRNG rng)
        {
            var chosen = new List<TilePosition>();

            for (var _ = 0; _ < CourageOrbCount; _++)
                chosen.Add(builder.ChooseNext(chosen, rng));

            return chosen;
        }

        private static TilePosition ChooseNext(this FloorTilesBuilder builder, IEnumerable<TilePosition> taken, IRNG rng)
        {
            Func<TilePosition, bool> isNotTaken = p => !taken.Contains(p);
            Func<TilePosition, float> weightFunction = p => CalculateCourageOrbWeight(p, taken);

            return
                builder.CourageOrbPositions
                .Where(isNotTaken)
                .GetWeightedRandom(weightFunction, rng);
        }

        internal static float CalculateCourageOrbWeight(TilePosition position, IEnumerable<TilePosition> taken)
        {
            var distanceOrigin = position.GetDistanceFromOrigin();
            var distanceToClosest = taken.Count() > 0 ? taken.Select(p => GetDistanceBetween(position, p)).Min() : 0;

            return (float)(Math.Pow(distanceOrigin, 2) + Math.Pow(distanceToClosest, 2));
        }

        private static float GetDistanceFromOrigin(this TilePosition position)
        {
            return (float)new Vector2(position.X, position.Y).Length();
        }

        private static float GetDistanceBetween(TilePosition p1, TilePosition p2)
        {
            return Vector2.Distance(
                new Vector2(p1.X, p1.Y),
                new Vector2(p2.X, p2.Y));
        }

    }

}