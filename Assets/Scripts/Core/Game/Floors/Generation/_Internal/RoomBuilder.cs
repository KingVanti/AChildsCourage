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
        private readonly IRoomRepository roomRepository;

        #endregion

        #region Constructors

        public RoomBuilder(ITileBuilder tileBuilder, IRoomRepository roomRepository)
        {
            this.tileBuilder = tileBuilder;
            this.roomRepository = roomRepository;
        }

        #endregion

        #region Methods

        public void Build(RoomInChunk roomInChunk)
        {
            var roomData = roomRepository.Load(roomInChunk.RoomId);
            var offset = roomInChunk.Position.GetTileOffset();

            Build(roomData, offset);
            OnRoomBuilt?.Invoke(this, new RoomBuiltEventArgs());
        }


        private void Build(RoomData roomData, TileOffset offset)
        {
            BuildWalls(roomData.WallPositions, offset);
            BuildGround(roomData.GroundPositions, offset);
        }

        private void BuildWalls(TilePosition[] wallPositions, TileOffset offset)
        {
            foreach (var wallPosition in wallPositions)
            {
                var offsetPosition = wallPosition + offset;

                tileBuilder.PlaceWall(offsetPosition);
            }
        }

        private void BuildGround(TilePosition[] groundPositions, TileOffset offset)
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