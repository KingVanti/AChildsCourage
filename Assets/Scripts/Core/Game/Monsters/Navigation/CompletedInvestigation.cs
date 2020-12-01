using System;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct CompletedInvestigation
    {

        public AOIIndex AOIIndex { get; }

        public DateTime CompletionTime { get; }


        public CompletedInvestigation(AOIIndex aoiIndex, DateTime completionTime)
        {
            AOIIndex = aoiIndex;
            CompletionTime = completionTime;
        }

    }

}