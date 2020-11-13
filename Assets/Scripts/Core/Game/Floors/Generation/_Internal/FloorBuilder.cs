using AChildsCourage.Game.Floors.Persistance;
using System;

namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class FloorBuilder : IFloorBuilder, IEagerActivation
    {

        #region Events

        public event EventHandler<FloorBuiltEventArgs> OnFloorBuilt;

        #endregion

        #region Fields

        private readonly IRoomRepository roomRepository;

        #endregion

        #region Constructors

        public FloorBuilder(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        #endregion

        #region Methods

        public void Build(FloorPlan floorPlan)
        {
            var floor = BuildFrom(roomRepository.LoadRoomsFor(floorPlan));

            OnFloorBuilt?.Invoke(this, new FloorBuiltEventArgs(floor));
        }

        internal Floor BuildFrom(RoomsInChunks roomsInChunks)
        {
            var floor = new Floor();

            foreach (var roomInChunk in roomsInChunks)
                BuildInto(roomInChunk, floor);

            floor.GenerateWalls();

            return floor;
        }

        private void BuildInto(RoomInChunk roomInChunk, Floor floor)
        {
            var tileOffset = roomInChunk.Position.GetTileOffset();

            PlaceRoomTilesInto(roomInChunk.Room.Tiles, tileOffset, floor);
        }

        private void PlaceRoomTilesInto(RoomTiles roomTiles, TileOffset tileOffset, Floor floor)
        {
            PlaceGroundInto(roomTiles.GroundTiles, tileOffset, floor);
        }

        private void PlaceGroundInto(Tiles<GroundTile> groundTiles, TileOffset tileOffset, Floor floor)
        {
            foreach (var groundTile in groundTiles)
                floor.PlaceGround(groundTile, tileOffset);
        }

        #endregion

    }

}