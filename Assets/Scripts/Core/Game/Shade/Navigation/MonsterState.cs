using System;
using static AChildsCourage.Game.MEntityPosition;
using static AChildsCourage.Game.Shade.Navigation.MInvestigationHistory;

namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct MonsterState
    {

        public EntityPosition Position { get; }

        public DateTime CurrentTime { get; }

        public MInvestigationHistory.InvestigationHistory InvestigationHistory { get; }


        public MonsterState(EntityPosition position, DateTime currentTime, MInvestigationHistory.InvestigationHistory investigationHistory)
        {
            Position = position;
            CurrentTime = currentTime;
            InvestigationHistory = investigationHistory;
        }

    }

}