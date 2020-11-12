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

        public void Build(Room room, ChunkPosition chunkPosition)
        {
            var offset = chunkPosition.GetTileOffset();

            Build(room, offset);
            OnRoomBuilt?.Invoke(this, new RoomBuiltEventArgs());
        }


        private void Build(Room room, TileOffset offset)
        {
            BuildGround(room.GroundTiles, offset);
        }

        private void BuildGround(Tiles<GroundTile> groundPositions, TileOffset offset)
        {
            foreach (var wallPosition in groundPositions)
            {
                var offsetPosition = wallPosition.Position + offset;

                tileBuilder.PlaceGround(offsetPosition);
            }
        }

        #endregion

    }

}