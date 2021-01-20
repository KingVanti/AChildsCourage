using System;

namespace AChildsCourage.Game.Char
{

    public class TensionLevelChangedEventArgs : EventArgs
    {

        internal TensionLevel Level { get; }


        internal TensionLevelChangedEventArgs(TensionLevel level) =>
            Level = level;

    }

}