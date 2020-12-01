using System;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct MonsterState
    {

        public EntityPosition Position { get; }

        public DateTime CurrentTime { get; }

        public InvestigationHistory InvestigationHistory { get; }


        public MonsterState(EntityPosition position, DateTime currentTime, InvestigationHistory investigationHistory)
        {
            Position = position;
            CurrentTime = currentTime;
            InvestigationHistory = investigationHistory;
        }

    }

}