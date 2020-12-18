using System;
using static AChildsCourage.Game.Shade.MAwareness;

namespace AChildsCourage.Game.Shade
{

    public class AwarenessChangedEventArgs : EventArgs
    {

        public Awareness NewAwareness { get; }

        public AwarenessLevel Level { get; }


        public AwarenessChangedEventArgs(Awareness newAwareness, AwarenessLevel level)
        {
            NewAwareness = newAwareness;
            Level = level;
        }

    }

}