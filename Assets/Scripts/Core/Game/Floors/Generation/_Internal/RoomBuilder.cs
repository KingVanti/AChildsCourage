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

        public void Build(RoomData roomData, ChunkPosition chunkPosition)
        {
            var offset = chunkPosition.GetTileOffset();

            Build(roomData, offset);
            OnRoomBuilt?.Invoke(this, new RoomBuiltEventArgs());
        }


        private void Build(RoomData roomData, TileOffset offset)
        {
            BuildGround(roomData.GroundPositions, offset);
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