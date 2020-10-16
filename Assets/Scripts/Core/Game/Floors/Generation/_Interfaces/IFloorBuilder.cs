using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public interface IFloorBuilder
    {

        #region Methods

        RoomBuildingSession StartBuilding();

        void PlaceWall(TilePosition position, RoomBuildingSession session);

        void PlaceFloor(TilePosition position, RoomBuildingSession session);

        #endregion

    }

}