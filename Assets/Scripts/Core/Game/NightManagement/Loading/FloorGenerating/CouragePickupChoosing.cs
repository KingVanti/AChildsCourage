using AChildsCourage.Game.Floors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class CouragePickupChoosing
    {

        internal const int CourageOrbCount = 10;
        internal const int CourageSparkCount = 25;


        internal static CouragePickupChooser GetDefault()
        {
            return floor =>
            {
                return Enumerable.Concat(
                    ChooseCourageOrbs(floor.CourageOrbPositions, CourageOrbCount),
                    ChooseCourageSparks(floor.CourageSparkPositions, CourageSparkCount));
            };
        }


        internal static IEnumerable<CouragePickup> ChooseCourageOrbs(IEnumerable<TilePosition> positions, int count)
        {
            Func<List<TilePosition>, List<TilePosition>> addNext = list =>
            {
                var next = ChooseNextOrbPosition(positions, list);
                list.Add(next);

                return list;
            };

            return
                Take(new List<TilePosition>())
                .RepeatFor(addNext, count)
                .Select(p => new CouragePickup(p, CourageVariant.Orb));
        }

        internal static TilePosition ChooseNextOrbPosition(IEnumerable<TilePosition> positions, IEnumerable<TilePosition> taken)
        {
            Func<TilePosition, bool> isNotTaken = p => !taken.Contains(p);
            Func<TilePosition, float> weight = p => CalculateCourageOrbWeight(p, taken);

            return
                Take(positions)
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


        internal static IEnumerable<CouragePickup> ChooseCourageSparks(IEnumerable<TilePosition> positions, int count)
        {
            Func<List<TilePosition>, List<TilePosition>> addNext = list =>
            {
                var next = ChooseNextSparkPosition(positions, list);
                list.Add(next);

                return list;
            };

            return
                Take(new List<TilePosition>())
                .RepeatFor(addNext, count)
                .Select(p => new CouragePickup(p, CourageVariant.Spark));
        }

        internal static TilePosition ChooseNextSparkPosition(IEnumerable<TilePosition> positions, IEnumerable<TilePosition> taken)
        {
            Func<TilePosition, bool> isNotTaken = p => !taken.Contains(p);
            Func<TilePosition, float> weight = p => CalculateCourageSparkWeight(p, taken);

            return
                Take(positions)
                .Where(isNotTaken)
                .OrderByDescending(weight)
                .First();
        }

        internal static float CalculateCourageSparkWeight(TilePosition position, IEnumerable<TilePosition> taken)
        {
            var distanceOrigin = GetDistanceFromOrigin(position);
            var distanceToClosest = taken.Count() > 0 ? taken.Select(p => GetDistanceBetween(position, p)).Min() : 0;

            var distanceOriginWeight = Math.Pow(distanceOrigin, 2);
            var distanceToClosestWeight = distanceToClosest > 0 ? 1f / distanceToClosest : 0;

            return (float)(distanceOriginWeight + distanceToClosestWeight);
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