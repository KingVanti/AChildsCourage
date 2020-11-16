using System;

namespace AChildsCourage.Game.Floors.Building
{

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
}