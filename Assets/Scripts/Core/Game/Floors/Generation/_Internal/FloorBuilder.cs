using System;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class FloorBuilder : IFloorBuilder
    {

        #region Events

        public event EventHandler<FloorPlacedEventArgs> OnFloorPlaced;

        #endregion

        #region Methods

        public RoomBuildingSession StartBuilding()
        {
            throw new NotImplementedException();
        }


        public void PlaceFloor(TilePosition position, RoomBuildingSession session)
        {
            OnFloorPlaced.Invoke(this, new FloorPlacedEventArgs(position));
        }


        public void PlaceWall(TilePosition position, RoomBuildingSession session)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}