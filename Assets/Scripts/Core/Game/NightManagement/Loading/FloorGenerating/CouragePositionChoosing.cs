using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class CouragePositionChoosing
    {

        internal const int CourageOrbCount = 5;


        internal static CouragePositionChooser GetDefault()
        {
            return floor =>
            {
                return ChooseCourageOrbPositions(floor.CourageOrbPositions, CourageOrbCount);
            };
        }


        internal static IEnumerable<TilePosition> ChooseCourageOrbPositions(IEnumerable<TilePosition> couragePositions, int orbCount)
        {
            Func<List<TilePosition>, List<TilePosition>> addNext = list =>
            {
                var next = ChooseNext(couragePositions, list);
                list.Add(next);

                return list;
            };

            return
                Pipe(new List<TilePosition>())
                .RepeatFor(addNext, orbCount);
        }

        internal static TilePosition ChooseNext(IEnumerable<TilePosition> positions, IEnumerable<TilePosition> taken)
        {
            Func<TilePosition, bool> isNotTaken = p => !taken.Contains(p);
            Func<TilePosition, float> weight = p => CalculateCourageOrbWeight(p, taken);

            return
                Pipe(positions)
                .Where(isNotTaken)
                .OrderByDescending(weight)
                .First();
        }

        internal static float CalculateCourageOrbWeight(TilePosition position, IEnumerable<TilePosition> taken)
        {
            var distanceOrigin = GetDistanceFromOrigin(position);
            var distanceToClosest = taken.Count() > 0 ? taken.Select(p => GetDistanceBetween(position, p)).Min() : 0;

            return (float)Math.Pow(distanceOrigin + distanceToClosest, 2);
        }

        internal static float GetDistanceFromOrigin(TilePosition position)
        {
            return (float)new Vector2(position.X, position.Y).Length();
        }

        internal static float GetDistanceBetween(TilePosition p1, TilePosition p2)
        {
            return Vector2.Distance(
                new Vector2(p1.X, p1.Y),
                new Vector2(p2.X, p2.Y));
        }

    }

}