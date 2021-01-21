using System;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CollectedCourageChangedEventArgs : EventArgs
    {

        internal float CompletionPercent { get; }


        internal CollectedCourageChangedEventArgs(float completionPercent) =>
            CompletionPercent = completionPercent;

    }

}