using System;

namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct ShadeState
    {

        public EntityPosition Position { get; }

        public DateTime CurrentTime { get; }

        public InvestigationHistory InvestigationHistory { get; }


        public ShadeState(EntityPosition position, DateTime currentTime, InvestigationHistory investigationHistory)
        {
            Position = position;
            CurrentTime = currentTime;
            InvestigationHistory = investigationHistory;
        }

    }

}