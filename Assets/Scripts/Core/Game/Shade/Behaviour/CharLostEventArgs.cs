using System;

namespace AChildsCourage.Game.Shade
{

    public class CharLostEventArgs : EventArgs
    {

        internal LastKnownCharInfo CharInfo { get; }


        internal CharLostEventArgs(LastKnownCharInfo charInfo) =>
            CharInfo = charInfo;

    }

}