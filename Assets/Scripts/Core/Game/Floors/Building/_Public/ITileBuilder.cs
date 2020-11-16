using System;

namespace AChildsCourage.Game.Floors.Building
{

    public interface ITileBuilder
    {

        #region Events

        event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        event EventHandler<WallPlacedEventArgs> OnWallPlaced;

        #endregion

    }

}