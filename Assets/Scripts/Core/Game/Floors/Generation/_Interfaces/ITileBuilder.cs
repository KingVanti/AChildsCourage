using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public interface ITileBuilder
    {

        #region Events

        event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        event EventHandler<WallPlacedEventArgs> OnWallPlaced;

        #endregion

        #region Methods

        void PlaceWall(TilePosition position);

        void PlaceGround(TilePosition position);

        #endregion

    }

}