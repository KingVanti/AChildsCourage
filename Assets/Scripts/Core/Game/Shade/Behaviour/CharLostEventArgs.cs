using System;

namespace AChildsCourage.Game.Shade
{

    public class CharLostEventArgs : EventArgs
    {

        public LastKnownCharInfo CharInfo { get; }

        
        public CharLostEventArgs(LastKnownCharInfo charInfo) =>
            CharInfo = charInfo;

    }

}