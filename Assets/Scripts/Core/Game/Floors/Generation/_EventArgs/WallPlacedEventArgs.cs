using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public class WallPlacedEventArgs : EventArgs
    {

        #region Properties

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        public WallPlacedEventArgs(TilePosition position)
        {
            Position = position;
        }

        #endregion

    }
}