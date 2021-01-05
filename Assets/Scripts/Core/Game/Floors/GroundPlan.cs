using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Shade;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct GroundPlan
    {

        public static GroundPlan CreateGroundPlan(Floor floor) =>
            floor.Map(Floor.GetPositionsOfType<GroundTileData>).Select(GetCenter)
                 .ToImmutableHashSet()
                 .Map(positions => new GroundPlan(positions));

        public static IEnumerable<Vector2> ChooseRandomAoiPositions(Rng.CreateRng rng, AoiGenParams @params, GroundPlan groundPlan)
        {
            var center = groundPlan.groundPositions
                                   .GetRandom(rng);
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

                var poi = circlePositions.GetRandom(rng);

                RemovePositionsFromCircle(poi);

                return list.Add(poi);
            }

            return ImmutableList<Vector2>.Empty.Cycle(AddPosition, @params.PoiCount);
        }


        private readonly ImmutableHashSet<Vector2> groundPositions;


        private GroundPlan(ImmutableHashSet<Vector2> groundPositions) =>
            this.groundPositions = groundPositions;

    }

}