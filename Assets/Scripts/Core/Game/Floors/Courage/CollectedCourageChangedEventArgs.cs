using System;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CollectedCourageChangedEventArgs : EventArgs
    {

        public int Collected { get; }


        public CollectedCourageChangedEventArgs(int collected) =>
            Collected = collected;

    }

}