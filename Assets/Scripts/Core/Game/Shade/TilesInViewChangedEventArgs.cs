using System;
using static AChildsCourage.Game.Shade.MTilesInView;

namespace AChildsCourage.Game.Shade
{

    public class TilesInViewChangedEventArgs : EventArgs
    {

        public TilesInView TilesInView { get; }
        

        public TilesInViewChangedEventArgs(TilesInView tilesInView) => TilesInView = tilesInView;

    }

}