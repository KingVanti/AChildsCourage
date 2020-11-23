using AChildsCourage.Game.Floors;
using System;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [Singleton]
    internal class FloorRecreator : IFloorRecreator
    {

        #region Events

        public event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        public event EventHandler<WallPlacedEventArgs> OnWallPlaced;
        public event EventHandler<CouragePlacedEventArgs> OnCouragePlaced;

        #endregion

        #region Methods

        public void Recreate(Floor floor)
        {
            floor.GroundTiles.ForEach(PlaceGround);
            floor.Walls.ForEach(PlaceWall);
            floor.CouragePickups.ForEach(PlaceCouragePickup);
        }

        internal void PlaceGround(GroundTile groundTile)
        {
            OnGroundPlaced?.Invoke(this, new GroundPlacedEventArgs(groundTile.Position));
        }

        internal void PlaceWall(Wall wall)
        {
            OnWallPlaced?.Invoke(this, new WallPlacedEventArgs(wall));
        }

        internal void PlaceCouragePickup(CouragePickup pickup)
        {
            OnCouragePlaced.Invoke(this, new CouragePlacedEventArgs(pickup.Position, pickup.Variant));
        }

        #endregion

    }

}