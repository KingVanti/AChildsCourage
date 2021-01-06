using System;
using AChildsCourage.Game.Char;

namespace AChildsCourage.Game.Shade
{

    public class CharVisibilityChangedEventArgs : EventArgs
    {

        public Visibility CharVisibility { get; }


        public CharVisibilityChangedEventArgs(Visibility charVisibility) =>
            CharVisibility = charVisibility;

    }

}