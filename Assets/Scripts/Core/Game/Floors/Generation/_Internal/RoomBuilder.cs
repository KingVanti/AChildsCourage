using AChildsCourage.Game.Floors.Persistance;
using System;

namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class RoomBuilder : IRoomBuilder
    {

        #region Events

        public event EventHandler<RoomBuiltEventArgs> OnRoomBuilt;

        #endregion

        #region Fields

        private readonly ITileBuilder tileBuilder;

        #endregion

        #region Constructors

        public RoomBuilder(ITileBuilder tileBuilder)
        {
            this.tileBuilder = tileBuilder;
        }

        #endregion

        #region Methods

        public void Build(RoomTiles tiles, ChunkPosition chunkPosition)
        {
            var offset = chunkPosition.GetTileOffset();

            Build(tiles, offset);
            OnRoomBuilt?.Invoke(this, new RoomBuiltEventArgs());
        }


        private void Build(RoomTiles tiles, TileOffset offset)
        {
            BuildGround(tiles.GroundPositions, offset);
        }

        private void BuildGround(PositionList groundPositions, TileOffset offset)
        {
            foreach (var wallPosition in groundPositions)
            {
                var offsetPosition = wallPosition + offset;

                tileBuilder.PlaceGround(offsetPosition);
            }
        }

        #endregion

    }

}