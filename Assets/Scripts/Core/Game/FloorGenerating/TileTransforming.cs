using static AChildsCourage.F;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        internal static TilePosition Transform(TilePosition position, ChunkTransform transform)
        {
            TilePosition Rotate(TilePosition p) => RotateClockwiseAround(p, transform.ChunkCenter);

            TilePosition Mirror(TilePosition p) => YMirrorOver(p, transform.ChunkCenter);

            return
                Take(position)
                    .MapWith(OffsetAround, transform.ChunkCorner)
                    .RepeatFor(Rotate, transform.RotationCount)
                    .DoIf(Mirror, transform.IsMirrored);
        }

        internal static TilePosition OffsetAround(TilePosition position, TilePosition chunkCorner) =>
            new TilePosition(
                chunkCorner.X + position.X,
                chunkCorner.Y + position.Y);

        internal static TilePosition YMirrorOver(TilePosition position, TilePosition chunkCenter)
        {
            var yDiff = chunkCenter.Y - position.Y;

            return new TilePosition(position.X, chunkCenter.Y + yDiff);
        }

        internal static TilePosition RotateClockwiseAround(TilePosition position, TilePosition chunkCenter)
        {
            var translated = new TilePosition(
                position.X - chunkCenter.X,
                position.Y - chunkCenter.Y);

            var rotated = new TilePosition(translated.Y, -translated.X);

            return new TilePosition(
                rotated.X + chunkCenter.X,
                rotated.Y + chunkCenter.Y);
        }

    }

}