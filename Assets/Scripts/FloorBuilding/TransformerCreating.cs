namespace AChildsCourage.Game.Floors
{

    public static partial class FloorBuilding
    {

        internal static TilePositionTransformer CreateTransformerFor(RoomInChunk room)
        {
            var tileOffset = GetTileOffsetFor(room);

            return new TilePositionTransformer(tileOffset);
        }

        internal static TileOffset GetTileOffsetFor(RoomInChunk room)
        {
            return GetTileOffsetFor(room.Position);
        }

        internal static TileOffset GetTileOffsetFor(ChunkPosition chunkPosition)
        {
            return new TileOffset(
                chunkPosition.X * ChunkPosition.ChunkTileSize,
                chunkPosition.Y * ChunkPosition.ChunkTileSize);
        }

    }

}