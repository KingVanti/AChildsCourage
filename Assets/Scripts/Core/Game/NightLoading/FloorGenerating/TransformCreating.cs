using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorGenerating
    {

        internal static ChunkTransform ToChunkTransform(RoomTransform transform)
        {
            var chunkCenter = GetChunkCenter(transform.Position);
            var chunkCorner = GetChunkCorner(transform.Position);

            return new ChunkTransform(transform.RotationCount, transform.IsMirrored, chunkCorner, chunkCenter);
        }

        internal static TilePosition GetChunkCenter(ChunkPosition chunkPosition)
        {
            return new TilePosition(
                chunkPosition.X * ChunkPosition.ChunkSize + ChunkPosition.ChunkExtent,
                chunkPosition.Y * ChunkPosition.ChunkSize + ChunkPosition.ChunkExtent);
        }

        internal static TilePosition GetChunkCorner(ChunkPosition chunkPosition)
        {
            return new TilePosition(
                chunkPosition.X * ChunkPosition.ChunkSize,
                chunkPosition.Y * ChunkPosition.ChunkSize);
        }

    }

}