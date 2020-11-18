namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static TilePositionTransformer CreateTransformerFor(RoomTransform transform)
        {
            var chunkCenter = GetChunkCenter(transform.Position);
            var chunkCorner = GetChunkCorner(transform.Position);

            return new TilePositionTransformer(transform.RotationCount, transform.IsMirrored, chunkCorner, chunkCenter);
        }

        internal static TilePosition GetChunkCenter(this ChunkPosition chunkPosition)
        {
            return new TilePosition(
                (chunkPosition.X * ChunkPosition.ChunkSize) + ChunkPosition.ChunkExtent,
                (chunkPosition.Y * ChunkPosition.ChunkSize) + ChunkPosition.ChunkExtent);
        }

        internal static TilePosition GetChunkCorner(this ChunkPosition chunkPosition)
        {
            return new TilePosition(
                chunkPosition.X * ChunkPosition.ChunkSize,
                chunkPosition.Y * ChunkPosition.ChunkSize);
        }

    }

}