using AChildsCourage.Game.Courage;
using System;

namespace AChildsCourage.Game.Floors.Building
{

    public class CouragePlacedEventArgs : EventArgs
    {
      
        #region Properties

        public TilePosition Position { get; }

        public CourageVariant Variant { get; }

        #endregion

        #region Constructors

        public CouragePlacedEventArgs(TilePosition position, CourageVariant variant)
        {
            Position = position;
            Variant = variant;
        }

        #endregion

    }
}