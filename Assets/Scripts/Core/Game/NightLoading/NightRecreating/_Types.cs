using AChildsCourage.Game.Floors;
using System;

namespace AChildsCourage.Game.NightLoading
{

    public class GroundPlacedEventArgs : EventArgs
    {

        #region Properties

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        public GroundPlacedEventArgs(TilePosition position)
        {
            Position = position;
        }

        #endregion

    }

    public class WallPlacedEventArgs : EventArgs
    {

        #region Properties

        public Wall Wall { get; }

        #endregion

        #region Constructors

        public WallPlacedEventArgs(Wall wall)
        {
            Wall = wall;
        }

        #endregion

    }

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