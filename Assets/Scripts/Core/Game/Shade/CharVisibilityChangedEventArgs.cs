using System;

namespace AChildsCourage.Game.Shade
{

    public class CharVisibilityChangedEventArgs : EventArgs
    {

        public Visibility CharVisibility { get; }


        public CharVisibilityChangedEventArgs(Visibility charVisibility) =>
            CharVisibility = charVisibility;

    }

}