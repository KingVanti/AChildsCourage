using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public interface IFloorBuilder
    {

        #region Events

        event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        event EventHandler<WallPlacedEventArgs> OnWallPlaced;

        #endregion

        #region Methods

        RoomBuildingSession StartBuilding();

        void PlaceWall(TilePosition position, RoomBuildingSession session);

        void PlaceGround(TilePosition position, RoomBuildingSession session);

        #endregion

    }

}