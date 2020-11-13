using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorBuiltEventArgs : EventArgs
    {
        
        #region Properties

        public Floor Floor { get; }

        #endregion

        #region Constructors

        public FloorBuiltEventArgs(Floor floor)
        {
            Floor = floor;
        }

        #endregion

    }

}