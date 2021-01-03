using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.Game.EntityPosition;
using static AChildsCourage.Game.Shade.Navigation.InvestigationHistory;
using static AChildsCourage.Rng;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct Investigation
    {

        private const float MinDistance = 30;
        private const float MaxDistance = 60;
        private const float MinTime = 1;
        private const float MaxTime = 300;
        private const float CompletionExplorationRation = 0.75f;


        // [0 .. 15]
        internal static CalculateAoiWeight CalcTotalWeight =>
            (aoi, monsterState) =>
                new[] {CalcDistanceWeight, CalcTimeWeight}.Sum(x => x(aoi, monsterState));

        // [0 .. 5]
        private static CalculateAoiWeight CalcDistanceWeight =>
            (aoi, monsterState) =>
                DistanceBetweenAoiAndMonster(aoi, monsterState)
                    .Clamp(MinDistance, MaxDistance)
                    .RemapSquared(MaxDistance, MinDistance, 0, 10);

        // [0 .. 10]
        private static CalculateAoiWeight CalcTimeWeight =>
            (aoi, monsterState) =>
                SecondsSinceLastVisit(aoi, monsterState)
                    .IfNull(MaxTime)
                    .Clamp(MinTime, MaxTime)
                    .Remap(MinTime, MaxTime, 0, 10);

        public static Investigation StartNew(FloorState floorState, ShadeState monsterState, CreateRng rng) =>
            new Investigation(ChooseAoi(floorState, monsterState, rng),
                              ImmutableHashSet<TilePosition>.Empty);

        public static bool IsComplete(Investigation investigation) =>
            ExplorationRatio(investigation) >= CompletionExplorationRation;

        private static float ExplorationRatio(Investigation investigation) =>
            InvestigatedPoiCount(investigation) / (float) PoiToInvestigateCount(investigation);

        private static int InvestigatedPoiCount(Investigation investigation) =>
            investigation.InvestigatedPositions.Count;

        private static int PoiToInvestigateCount(Investigation investigation) =>
            investigation.Aoi.Pois.Length;


        public static Investigation Progress(Investigation investigation, IEnumerable<TilePosition> positions) =>
            new Investigation(investigation.Aoi,
                              investigation.InvestigatedPositions.Union(positions.Where(p => IsPartOfInvestigation(investigation, p))));

        private static bool IsPartOfInvestigation(Investigation investigation, TilePosition position) =>
            investigation.Aoi.Pois.Any(poi => poi.Position.Equals(position));

        public static TilePosition NextTarget(Investigation investigation, EntityPosition monsterPosition) =>
            UninvestigatedPois(investigation)
                .OrderBy(poi => DistanceTo(poi.Position, GetEntityTile(monsterPosition)))
                .First().Position;

        private static IEnumerable<Poi> UninvestigatedPois(Investigation investigation) =>
            investigation.Aoi.Pois
                         .Where(poi => !investigation.InvestigatedPositions.Contains(poi.Position));

        public static CompletedInvestigation Complete(Investigation investigation) =>
            new CompletedInvestigation(investigation.Aoi.Index, DateTime.Now);

        private static Aoi ChooseAoi(FloorState floorState, ShadeState monsterState, CreateRng rng) =>
            floorState.AOIs.GetWeightedRandom(aoi => CalcTotalWeight(aoi, monsterState), rng);

        private static float DistanceBetweenAoiAndMonster(Aoi aoi, ShadeState monsterState) =>
            DistanceTo(aoi.Center, GetEntityTile(monsterState.Position));

        private static float? SecondsSinceLastVisit(Aoi aoi, ShadeState monsterState) =>
            monsterState.InvestigationHistory.Map(FindInHistory, aoi.Index)
                        .Bind(i => monsterState.CurrentTime - i.CompletionTime)
                        .Bind(time => (float) time.TotalSeconds);


        internal delegate float CalculateAoiWeight(Aoi aoi, ShadeState shadeState);

        internal Aoi Aoi { get; }

        internal ImmutableHashSet<TilePosition> InvestigatedPositions { get; }


        internal Investigation(Aoi aoi, ImmutableHashSet<TilePosition> investigatedPositions)
        {
            Aoi = aoi;
            InvestigatedPositions = investigatedPositions;
        }

    }

}