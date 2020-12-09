using System;

namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct CompletedInvestigation
    {

        public AoiIndex AoiIndex { get; }

        public DateTime CompletionTime { get; }


        public CompletedInvestigation(AoiIndex aoiIndex, DateTime completionTime)
        {
            AoiIndex = aoiIndex;
            CompletionTime = completionTime;
        }

    }

}