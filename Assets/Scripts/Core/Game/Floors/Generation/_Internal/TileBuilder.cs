using System;

namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    public class TileBuilder : ITileBuilder
    {

        #region Events

        public event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        public event EventHandler<WallPlacedEventArgs> OnWallPlaced;

        #endregion

        #region Methods

        public void PlaceGround(TilePosition position)
        {
            OnGroundPlaced.Invoke(this, new GroundPlacedEventArgs(position));
        }


        public void PlaceWall(TilePosition position)
        {
            OnWallPlaced.Invoke(this, new WallPlacedEventArgs(position));
        }

        #endregion

    }

}