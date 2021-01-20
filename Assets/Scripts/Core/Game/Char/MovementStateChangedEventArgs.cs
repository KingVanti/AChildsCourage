using System;

namespace AChildsCourage.Game.Char
{

    public class MovementStateChangedEventArgs : EventArgs
    {

        internal MovementState Current { get; }

        internal MovementState Previous { get; }


        internal MovementStateChangedEventArgs(MovementState current, MovementState previous)
        {
            Current = current;
            Previous = previous;
        }

    }

}