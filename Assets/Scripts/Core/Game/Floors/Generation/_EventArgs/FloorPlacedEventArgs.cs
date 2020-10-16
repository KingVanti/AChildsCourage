using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorPlacedEventArgs : EventArgs
    {

        #region Properties

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        public FloorPlacedEventArgs(TilePosition position)
        {
            Position = position;
        }

        #endregion

    }
}