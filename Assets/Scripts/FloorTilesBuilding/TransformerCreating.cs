namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static TilePositionTransformer CreateTransformerFor(RoomInChunk room)
        {
            var tileOffset = GetTileOffsetFor(room.Position);

            return new TilePositionTransformer(tileOffset);
        }

        internal static TileOffset GetTileOffsetFor(ChunkPosition chunkPosition)
        {
            return new TileOffset(
                chunkPosition.X * ChunkPosition.ChunkTileSize,
                chunkPosition.Y * ChunkPosition.ChunkTileSize);
        }

    }

}