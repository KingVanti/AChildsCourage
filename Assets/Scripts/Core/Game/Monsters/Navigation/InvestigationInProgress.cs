using System;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct InvestigationInProgress
    {

        public static StartInvestigation StartNew = (aois, monsterState) => throw new NotImplementedException();

        public static InvestigationIsComplete IsComplete = investigation => throw new NotImplementedException();

        public static ProgressInvestigation Progress = (investigation, positions) => throw new NotImplementedException();

        public static CompleteInvestigation Complete = investigation => throw new NotImplementedException();


        internal AOI AOI { get; }

        public TilePosition TargetPosition { get; }

        internal ImmutableList<TilePosition> InvestigatedPositions { get; }

    }

}