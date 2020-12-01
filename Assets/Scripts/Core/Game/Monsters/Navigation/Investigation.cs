using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.CustomMath;
using static AChildsCourage.RNG;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public static class MInvestigation
    {

        #region Records

        public readonly struct Investigation
        {

            internal AOI AOI { get; }

            internal ImmutableHashSet<TilePosition> InvestigatedPositions { get; }


            internal Investigation(AOI aoi, ImmutableHashSet<TilePosition> investigatedPositions)
            {
                AOI = aoi;
                InvestigatedPositions = investigatedPositions;
            }

        }

        #endregion


        #region Functions

        public static StartInvestigation StartNew =>
            (floorState, monsterState, rng) =>
                new Investigation(
                    ChooseAOI(floorState, monsterState, rng),
                    ImmutableHashSet<TilePosition>.Empty);

        
        public static InvestigationIsComplete IsComplete =>
            investigation =>
                ExplorationRatio(investigation) >= CompletionExplorationRation;

        private static Func<Investigation, float> ExplorationRatio =>
            investigation =>
                InvestigatedPOICount(investigation) / (float) POIToInvestigateCount(investigation);

        private static Func<Investigation, int> InvestigatedPOICount =>
            investigation =>
                investigation.InvestigatedPositions.Count;

        private static Func<Investigation, int> POIToInvestigateCount =>
            investigation =>
                investigation.AOI.POIs.Length;


        public static ProgressInvestigation Progress =>
            (investigation, positions) =>
                new Investigation(
                    investigation.AOI,
                    investigation.InvestigatedPositions.Union(
                        positions.Where(p => IsPartOfInvestigation(investigation, p))));

        private static Func<Investigation, TilePosition, bool> IsPartOfInvestigation =>
            (investigation, position) =>
                investigation.AOI.POIs.Any(poi => poi.Position.Equals(position));


        public static ChooseNextTarget NextTarget =>
            (investigation, monsterPosition) =>
                UninvestigatedPOIs(investigation)
                    .OrderBy(poi => GetDistanceBetween(poi.Position, EntityPosition.GetTilePosition(monsterPosition)))
                    .First().Position;

        private static Func<Investigation, IEnumerable<POI>> UninvestigatedPOIs =>
            investigation =>
                investigation.AOI.POIs
                             .Where(poi => !investigation.InvestigatedPositions.Contains(poi.Position));

        
        public static CompleteInvestigation Complete =>
            investigation =>
                new CompletedInvestigation(investigation.AOI.Index, DateTime.Now);


        private static ChooseInvestigationAOI ChooseAOI =>
            (floorState, monsterState, rng) =>
                floorState.AOIs.GetWeightedRandom(aoi => CalcTotalWeight(aoi, monsterState), rng);


        // [0 .. 15]
        internal static CalculateAOIWeight CalcTotalWeight =>
            (aoi, monsterState) =>
                new[] { CalcDistanceWeight, CalcTimeWeight }.Sum(x => x(aoi, monsterState));

        // [0 .. 5]
        private static CalculateAOIWeight CalcDistanceWeight =>
            (aoi, monsterState) =>
                GetDistanceBetween(aoi.Center, EntityPosition.GetTilePosition(monsterState.Position))
                    .Clamp(MinDistance, MaxDistance)
                    .Map(distance => Map(distance, MinDistance, MaxDistance, 5, 0));

        // [0 .. 10]
        private static CalculateAOIWeight CalcTimeWeight =>
            (aoi, monsterState) =>
                InvestigationHistory.FindInvestigation(monsterState.InvestigationHistory, aoi.Index)
                                    .Bind(i => monsterState.CurrentTime - i.CompletionTime)
                                    .Bind(time => (float) time.TotalSeconds).IfNull(MaxTime)
                                    .Map(seconds => seconds.Clamp(MinTime, MaxTime))
                                    .Map(seconds => Map(seconds, MinTime, MaxTime, 0, 10));

        #endregion

        #region Values

        private const float MinDistance = 10;
        private const float MaxDistance = 100;
        private const float MinTime = 1;
        private const float MaxTime = 300;
        private const float CompletionExplorationRation = 0.5f;

        #endregion

        #region Delegates

        public delegate Investigation StartInvestigation(FloorState floorState, MonsterState monsterState, CreateRNG rng);

        public delegate Investigation ProgressInvestigation(Investigation investigation, IEnumerable<TilePosition> investigatedPositions);

        public delegate bool InvestigationIsComplete(Investigation investigation);

        public delegate CompletedInvestigation CompleteInvestigation(Investigation investigation);

        public delegate TilePosition ChooseNextTarget(Investigation investigation, EntityPosition monsterPosition);

        internal delegate AOI ChooseInvestigationAOI(FloorState floorState, MonsterState monsterState, CreateRNG rng);

        internal delegate float CalculateAOIWeight(AOI aoi, MonsterState monsterState);

        #endregion

    }

}