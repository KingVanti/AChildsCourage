using AChildsCourage.Game.Floors.Persistance;
using System;

namespace AChildsCourage.Game.Floors.Generation
{

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

        public void Build(RoomAtPosition roomAtPosition)
        {
            var roomData = roomRepository.Load(roomAtPosition.RoomId);

            var room = Build(roomData, roomAtPosition.Position);

            OnRoomBuilt?.Invoke(this, new RoomBuiltEventArgs());
        }

        private IRoom Build(RoomData roomData, TilePosition position)
        {
            BuildWalls(roomData.WallPositions, position);
            BuildGround(roomData.GroundPositions, position);

            return null;
        }

        private void BuildWalls(TilePosition[] wallPositions, TilePosition roomPosition)
        {
            foreach (var wallPosition in wallPositions)
            {
                var offsetPosition = wallPosition + roomPosition;

                tileBuilder.PlaceWall(offsetPosition);
            }
        }

        private void BuildGround(TilePosition[] groundPositions, TilePosition roomPosition)
        {
            foreach (var wallPosition in groundPositions)
            {
                var offsetPosition = wallPosition + roomPosition;

                tileBuilder.PlaceGround(offsetPosition);
            }
        }

        #endregion

    }

}