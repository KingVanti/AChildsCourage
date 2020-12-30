using System;

namespace AChildsCourage.Game.Input
{

    internal class SprintInputEventArgs : EventArgs
    {

        public bool HasSprintInput { get; }


        public SprintInputEventArgs(bool hasSprintInput) =>
            HasSprintInput = hasSprintInput;

    }

}