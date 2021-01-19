using System;

namespace AChildsCourage.Game.Char
{

    public class TensionLevelChangedEventArgs : EventArgs
    {

        public TensionLevel Level { get; }


        public TensionLevelChangedEventArgs(TensionLevel level) =>
            Level = level;

    }

}