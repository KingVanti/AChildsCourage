using System;

namespace AChildsCourage.Game.Shade
{

    public class ShadeCommandEventArgs : EventArgs
    {

        internal ShadeCommand Command { get; }


        internal ShadeCommandEventArgs(ShadeCommand command) =>
            Command = command;

    }

}