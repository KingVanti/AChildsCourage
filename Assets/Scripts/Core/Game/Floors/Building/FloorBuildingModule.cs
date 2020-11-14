using static AChildsCourage.Game.Floors.Generation.GenerationModule;

namespace AChildsCourage.Game.Floors.Building
{

    public static class FloorBuildingModule
    {

        #region Methods

        internal static Floor BuildFrom(RoomsInChunks roomsInChunks)
        {
            var buildingSession = new FloorBuildingSession();

            foreach (var roomInChunk in roomsInChunks)
                BuildInto(roomInChunk, buildingSession);

            buildingSession.GenerateWalls();

            return buildingSession.BuildFloor();
        }

        private static void BuildInto(RoomInChunk roomInChunk, FloorBuildingSession buildingSession)
        {
            var tileOffset = GetTileOffset(roomInChunk);

            PlaceRoomTilesInto(roomInChunk.Room.Tiles, tileOffset, buildingSession);
        }

        private static TileOffset GetTileOffset(RoomInChunk roomInChunk)
        {
            return GetTileOffsetFor(roomInChunk.Position);
        }

        private static void PlaceRoomTilesInto(RoomTiles roomTiles, TileOffset tileOffset, FloorBuildingSession buildingSession)
        {
            PlaceGroundInto(roomTiles.GroundTiles, tileOffset, buildingSession);
        }

        private static void PlaceGroundInto(Tiles<GroundTile> groundTiles, TileOffset tileOffset, FloorBuildingSession buildingSession)
        {
            foreach (var groundTile in groundTiles)
                buildingSession.PlaceGround(groundTile, tileOffset);
        }

        #endregion

    }

}