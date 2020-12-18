using System;

namespace AChildsCourage.Game.Char
{

    public class MovementStateChangedEventArgs : EventArgs
    {

        public MovementState MovementState { get; }
        

        public MovementStateChangedEventArgs(MovementState movementState) => MovementState = movementState;

    }

}