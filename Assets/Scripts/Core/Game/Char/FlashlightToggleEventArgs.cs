using System;

namespace AChildsCourage.Game.Char
{

    public class FlashlightToggleEventArgs : EventArgs
    {

        internal bool IsTurnedOn { get; }


        internal FlashlightToggleEventArgs(bool isTurnedOn) =>
            IsTurnedOn = isTurnedOn;

    }

}