using AChildsCourage.Game.Floors.Persistance;
using System;

using static AChildsCourage.Game.Floors.Generation.Generation;

namespace AChildsCourage.Game.Floors.Building
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

        internal FloorBuilder(IRoomRepository roomRepository)
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
            var buildingSession = new FloorBuildingSession();

            foreach (var roomInChunk in roomsInChunks)
                BuildInto(roomInChunk, buildingSession);

            buildingSession.GenerateWalls();

            return buildingSession.BuildFloor();
        }

        private void BuildInto(RoomInChunk roomInChunk, FloorBuildingSession buildingSession)
        {
            var tileOffset = GetTileOffset(roomInChunk);

            PlaceRoomTilesInto(roomInChunk.Room.Tiles, tileOffset, buildingSession);
        }

        private TileOffset GetTileOffset(RoomInChunk roomInChunk)
        {
            return GetTileOffsetFor(roomInChunk.Position); 
        }

        private void PlaceRoomTilesInto(RoomTiles roomTiles, TileOffset tileOffset, FloorBuildingSession buildingSession)
        {
            PlaceGroundInto(roomTiles.GroundTiles, tileOffset, buildingSession);
        }

        private void PlaceGroundInto(Tiles<GroundTile> groundTiles, TileOffset tileOffset, FloorBuildingSession buildingSession)
        {
            foreach (var groundTile in groundTiles)
                buildingSession.PlaceGround(groundTile, tileOffset);
        }

        #endregion

    }

}