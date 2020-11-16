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

        #endregion

        #region Methods

        [EventSubscription(nameof(INightLoader.OnFloorBuilt), typeof(OnPublisher))]
        public void OnFloorBuilt(FloorBuiltEventArgs eventArgs)
        {
            PlacesTilesFor(eventArgs.Floor);
        }

        internal void PlacesTilesFor(Floor floor)
        {
            foreach (var groundTilePosition in floor.GroundTilePositions)
                PlaceGround(groundTilePosition);

            foreach (var wall in floor.Walls)
                PlaceWall(wall);
        }

        internal void PlaceGround(TilePosition position)
        {
            OnGroundPlaced?.Invoke(this, new GroundPlacedEventArgs(position));
        }

        internal void PlaceWall(Wall wall)
        {
            OnWallPlaced?.Invoke(this, new WallPlacedEventArgs(wall));
        }

        #endregion

    }

}