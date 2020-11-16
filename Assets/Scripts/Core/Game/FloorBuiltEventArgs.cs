using AChildsCourage.Game.Floors;
using System;

namespace AChildsCourage.Game
{

    internal class FloorBuiltEventArgs : EventArgs
    {

        #region Properties

        internal Floor Floor { get; }

        #endregion

        #region Constructors

        internal FloorBuiltEventArgs(Floor floor)
        {
            Floor = floor;
        }

        #endregion

    }

}