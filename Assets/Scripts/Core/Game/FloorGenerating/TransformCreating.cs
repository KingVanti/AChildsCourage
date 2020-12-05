using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class MFloorGenerating
    {

        internal static ChunkTransform ToChunkTransform(RoomTransform transform)
        {
            var chunkCenter = GetChunkCenter(transform.Position);
            var chunkCorner = GetChunkCorner(transform.Position);

            return new ChunkTransform(transform.RotationCount, transform.IsMirrored, chunkCorner, chunkCenter);
        }

        internal static TilePosition GetChunkCenter(ChunkPosition chunkPosition) =>
            new TilePosition(
                chunkPosition.X * ChunkSize + ChunkExtent,
                chunkPosition.Y * ChunkSize + ChunkExtent);

        internal static TilePosition GetChunkCorner(ChunkPosition chunkPosition) =>
            new TilePosition(
                chunkPosition.X * ChunkSize,
                chunkPosition.Y * ChunkSize);

    }

}