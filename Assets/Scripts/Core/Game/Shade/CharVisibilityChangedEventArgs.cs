using System;
using AChildsCourage.Game.Char;

namespace AChildsCourage.Game.Shade
{

    public class CharVisibilityChangedEventArgs : EventArgs
    {

        internal Visibility CharVisibility { get; }


        internal CharVisibilityChangedEventArgs(Visibility charVisibility) =>
            CharVisibility = charVisibility;

    }

}