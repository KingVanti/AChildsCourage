using System;

namespace AChildsCourage.Game.Char
{

    public class MovementStateChangedEventArgs : EventArgs
    {

        public MovementState Current { get; }

        public MovementState Previous { get; }


        public MovementStateChangedEventArgs(MovementState current, MovementState previous)
        {
            Current = current;
            Previous = previous;
        }

    }

}