using AChildsCourage.Game.Floors;
using System;

namespace AChildsCourage.Game.NightManagement.Loading
{

    public interface IFloorRecreator
    {

        #region Events

        event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        event EventHandler<WallPlacedEventArgs> OnWallPlaced;
        event EventHandler<CouragePlacedEventArgs> OnCouragePlaced;

        #endregion

        #region Methods

        void Recreate(Floor floor);

        #endregion

    }

}