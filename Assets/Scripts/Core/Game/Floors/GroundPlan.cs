using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Shade;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    internal readonly struct GroundPlan
    {

        internal static GroundPlan emptyGroundPlan = new GroundPlan(ImmutableHashSet<Vector2>.Empty);

        internal static GroundPlan CreateGroundPlan(Floor floor) =>
            floor.Map(Floor.GetPositionsOfType<GroundTileData>).Select(GetCenter)
                 .ToImmutableHashSet()
                 .Map(positions => new GroundPlan(positions));

        internal static IEnumerable<Vector2> ChooseRandomAoiPositions(Rng rng, AoiGenParams @params, GroundPlan groundPlan)
        {
            var center = groundPlan.groundPositions
                                   .TryGetRandom(rng, () => throw new Exception("Ground-plan is empty!"));

            return groundPlan.Map(ChooseRandomAoiPositionsWithCenter, center, rng, @params);
        }

        internal static IEnumerable<Vector2> ChooseRandomAoiPositionsWithCenter(Vector2 center, Rng rng, AoiGenParams @params, GroundPlan groundPlan)
        {
            var circlePositions = groundPlan.groundPositions
                                            .Where(p => Vector2.Distance(p, center) < @params.Radius)
                                            .ToImmutableHashSet();

            void RemovePositionsFromCircle(Vector2 poi) =>
                circlePositions = circlePositions
                                  .Where(p => Vector2.Distance(poi, p) > @params.PoiDistance)
                                  .ToImmutableHashSet();

            ImmutableList<Vector2> AddPosition(ImmutableList<Vector2> list)
            {
                if (circlePositions.IsEmpty) return list;

                var poi = circlePositions.TryGetRandom(rng, () => throw new Exception("Has no positions left!"));

                RemovePositionsFromCircle(poi);

                return list.Add(poi);
            }

            return ImmutableList<Vector2>.Empty.Cycle(AddPosition, @params.PoiCount);
        }

        internal static Vector2 ChooseRandomPositionOutsideRadius(Rng rng, float radius, GroundPlan groundPlan) =>
            groundPlan.groundPositions
                      .Where(p => p.magnitude >= radius)
                      .TryGetRandom(rng, () => groundPlan.Map(ChooseRandomPositionOutsideRadius, rng, radius * 0.75f));


        private readonly ImmutableHashSet<Vector2> groundPositions;


        private GroundPlan(ImmutableHashSet<Vector2> groundPositions) =>
            this.groundPositions = groundPositions;

    }

}