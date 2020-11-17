using AChildsCourage.Game.Floors;
using System;

namespace AChildsCourage.Game
{

    internal class FloorBuiltEventArgs : EventArgs
    {

        #region Properties

        internal FloorTiles Floor { get; }

        #endregion

        #region Constructors

        internal FloorBuiltEventArgs(FloorTiles floor)
        {
            Floor = floor;
        }

        #endregion

    }

}