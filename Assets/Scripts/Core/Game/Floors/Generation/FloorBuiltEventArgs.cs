using System;

namespace AChildsCourage.Game.Floors.Generation
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