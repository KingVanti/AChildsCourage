using System;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CollectedCourageChangedEventArgs : EventArgs
    {

        public int Collected { get; }

        public float CompletionPercent { get; }


        public CollectedCourageChangedEventArgs(int collected, float completionPercent)
        {
            Collected = collected;
            CompletionPercent = completionPercent;
        }

    }

}