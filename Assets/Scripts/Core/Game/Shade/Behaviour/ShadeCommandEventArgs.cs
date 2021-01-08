using System;

namespace AChildsCourage.Game.Shade
{

    public class ShadeCommandEventArgs : EventArgs
    {

        public ShadeCommand Command { get; }

        
        public ShadeCommandEventArgs(ShadeCommand command) =>
            Command = command;

    }

}