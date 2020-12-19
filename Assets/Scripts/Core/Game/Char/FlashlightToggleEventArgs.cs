using System;

namespace AChildsCourage.Game.Char
{

    public class FlashlightToggleEventArgs : EventArgs
    {

        public bool IsTurnedOn { get; }


        public FlashlightToggleEventArgs(bool isTurnedOn) => IsTurnedOn = isTurnedOn;

    }

}