using System;
using static AChildsCourage.Game.Shade.MVisibility;

namespace AChildsCourage.Game.Shade
{

    public class CharVisibilityChangedEventArgs : EventArgs
    {

        public Visibility CharVisibility { get; }


        public CharVisibilityChangedEventArgs(Visibility charVisibility) => CharVisibility = charVisibility;

    }

}