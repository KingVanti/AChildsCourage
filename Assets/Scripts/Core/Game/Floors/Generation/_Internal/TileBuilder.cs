using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
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

        [EventSubscription(nameof(IFloorBuilder.OnFloorBuilt), typeof(OnPublisher))]
        public void OnFloorBuilt(FloorBuiltEventArgs eventArgs)
        {
            PlacesTilesFor(eventArgs.Floor);
        }

        internal void PlacesTilesFor(Floor floor)
        {
            foreach (var groundTilePosition in floor.GroundTilePositions)
                PlaceGround(groundTilePosition);

            foreach (var wallTilePosition in floor.WallTilePositions)
                PlaceWall(wallTilePosition);
        }

        internal void PlaceGround(TilePosition position)
        {
            OnGroundPlaced?.Invoke(this, new GroundPlacedEventArgs(position));
        }

        internal void PlaceWall(TilePosition position)
        {
            OnWallPlaced?.Invoke(this, new WallPlacedEventArgs(position));
        }

        #endregion

    }

}