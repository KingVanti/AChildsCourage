using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.Building
{

    internal static class FloorBuildingModule
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
            var transformer = new TilePositionTransformer(tileOffset);

            PlaceRoomTilesInto(roomInChunk.Room.Tiles, transformer, buildingSession);
        }

        private static TileOffset GetTileOffset(RoomInChunk roomInChunk)
        {
            return GetTileOffsetFor(roomInChunk.Position);
        }

        private static void PlaceRoomTilesInto(RoomTiles roomTiles, TilePositionTransformer transformer, FloorBuildingSession buildingSession)
        {
            PlaceGroundInto(roomTiles.GroundTiles, transformer, buildingSession);
        }

        private static void PlaceGroundInto(Tiles<GroundTile> groundTiles, TilePositionTransformer transformer, FloorBuildingSession buildingSession)
        {
            foreach (var groundTile in groundTiles)
            {
                var transformed = new GroundTile(transformer.Transform(groundTile.Position), groundTile.DistanceToWall, groundTile.AOIIndex);
                buildingSession.PlaceGround(transformed);
            }
        }


        internal static IEnumerable<TilePosition> GenerateWallsFor(HashSet<TilePosition> groundPositions)
        {
            var unfilteredPositions = GetUnfilteredWallPositions(groundPositions);
            Func<TilePosition, bool> isEmpty = p => !groundPositions.Contains(p);

            return unfilteredPositions.Where(isEmpty);
        }

        private static IEnumerable<TilePosition> GetUnfilteredWallPositions(HashSet<TilePosition> groundPositions)
        {
            foreach (var groundPosition in groundPositions)
                for (var dX = -1; dX <= 1; dX++)
                    for (var dY = -1; dY <= 3; dY++)
                        if (dX != 0 || dY != 0)
                            yield return groundPosition + new TileOffset(dX, dY);
        }


        internal static TileOffset GetTileOffsetFor(ChunkPosition chunkPosition)
        {
            return new TileOffset(
                chunkPosition.X * ChunkPosition.ChunkTileSize,
                chunkPosition.Y * ChunkPosition.ChunkTileSize);
        }

        #endregion

    }

}