using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.CustomMath;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct Investigation
    {

        private const float MinDistance = 10;
        private const float MaxDistance = 100;
        private const float MinTime = 1;
        private const float MaxTime = 300;


        public delegate Investigation StartInvestigation(FloorState floorState, MonsterState monsterState, CreateRNG rng);

        public delegate Investigation ProgressInvestigation(Investigation investigation, IEnumerable<TilePosition> investigatedPositions);

        public delegate bool InvestigationIsComplete(Investigation investigation);

        public delegate CompletedInvestigation CompleteInvestigation(Investigation investigation);

        public delegate TilePosition ChooseNextTarget(Investigation investigation);

        internal delegate AOI ChooseInvestigationAOI(FloorState floorState, MonsterState monsterState, CreateRNG rng);

        internal delegate float CalculateAOIWeight(AOI aoi, MonsterState monsterState);


        public static StartInvestigation StartNew =>
            (floorState, monsterState, rng) =>
                new Investigation(ChooseAOI(floorState, monsterState, rng), ImmutableList<TilePosition>.Empty);

        public static InvestigationIsComplete IsComplete => investigation => throw new NotImplementedException();

        public static ProgressInvestigation Progress => (investigation, positions) => throw new NotImplementedException();

        public static ChooseNextTarget NextTarget => investigation => throw new NotImplementedException();

        public static CompleteInvestigation Complete => investigation => throw new NotImplementedException();


        private static ChooseInvestigationAOI ChooseAOI =>
            (floorState, monsterState, rng) =>
                floorState.AOIs.GetWeightedRandom(aoi => CalcTotalWeight(aoi, monsterState), rng);


        // [0 .. 15]
        internal static CalculateAOIWeight CalcTotalWeight => (aoi, monsterState) => new[] { CalcDistanceWeight, CalcTimeWeight }.Sum(x => x(aoi, monsterState));

        // [0 .. 5]
        private static CalculateAOIWeight CalcDistanceWeight =>
            (aoi, monsterState) =>
                TilePosition.GetDistanceBetween(aoi.Center, EntityPosition.GetTilePosition(monsterState.Position))
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

        internal AOI AOI { get; }

        internal ImmutableList<TilePosition> InvestigatedPositions { get; }


        private Investigation(AOI aoi, ImmutableList<TilePosition> investigatedPositions)
        {
            AOI = aoi;
            InvestigatedPositions = investigatedPositions;
        }

    }

}