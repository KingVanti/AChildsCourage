using System;

namespace AChildsCourage.Game.Shade
{

    public class TilesInViewChangedEventArgs : EventArgs
    {

        public TilesInView TilesInView { get; }


        public TilesInViewChangedEventArgs(TilesInView tilesInView) =>
            TilesInView = tilesInView;

    }

}