using System;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class FloorBuilder : IFloorBuilder
    {

        #region Events

        public event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        public event EventHandler<WallPlacedEventArgs> OnWallPlaced;

        #endregion

        #region Methods

        public RoomBuildingSession StartBuilding()
        {
            throw new NotImplementedException();
        }


        public void PlaceGround(TilePosition position, RoomBuildingSession session)
        {
            OnGroundPlaced.Invoke(this, new GroundPlacedEventArgs(position));
        }


        public void PlaceWall(TilePosition position, RoomBuildingSession session)
        {
            OnWallPlaced.Invoke(this, new WallPlacedEventArgs(position));
        }

        #endregion

    }

}