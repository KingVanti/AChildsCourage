using System;

namespace AChildsCourage.Game.Shade
{

    public class AwarenessChangedEventArgs : EventArgs
    {

        internal Awareness NewAwareness { get; }

        internal AwarenessLevel Level { get; }


        internal AwarenessChangedEventArgs(Awareness newAwareness, AwarenessLevel level)
        {
            NewAwareness = newAwareness;
            Level = level;
        }

    }

}