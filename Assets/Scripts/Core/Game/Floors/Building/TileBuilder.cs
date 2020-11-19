using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;

namespace AChildsCourage.Game.Floors.Building
{

    [Singleton]
    internal class TileBuilder : ITileBuilder
    {

        #region Events

        public event EventHandler<GroundPlacedEventArgs> OnGroundPlaced;
        public event EventHandler<WallPlacedEventArgs> OnWallPlaced;
        public event EventHandler<CouragePlacedEventArgs> OnCouragePlaced;

        #endregion

        #region Methods

        [EventSubscription(nameof(INightLoader.OnFloorBuilt), typeof(OnPublisher))]
        public void OnFloorBuilt(FloorBuiltEventArgs eventArgs)
        {
            PlacesTilesFor(eventArgs.Floor);
        }

        internal void PlacesTilesFor(FloorTiles floor)
        {
            floor.GroundTilePositions.ForEach(PlaceGround);
            floor.Walls.ForEach(PlaceWall);
            floor.CourageOrbPositions.ForEach(PlaceCourageOrb);
        }

        internal void PlaceGround(TilePosition position)
        {
            OnGroundPlaced?.Invoke(this, new GroundPlacedEventArgs(position));
        }

        internal void PlaceWall(Wall wall)
        {
            OnWallPlaced?.Invoke(this, new WallPlacedEventArgs(wall));
        }

        internal void PlaceCourageOrb(TilePosition position)
        {
            OnCouragePlaced.Invoke(this, new CouragePlacedEventArgs(position, Courage.CourageVariant.Orb));
        }

        #endregion

    }

}