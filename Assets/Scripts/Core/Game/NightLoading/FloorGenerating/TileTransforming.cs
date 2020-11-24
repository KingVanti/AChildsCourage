using System;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightLoading
{

    internal static class TileTransforming
    {

        internal static TileTransformer GetDefault(ChunkTransform transform)
        {
            return position =>
            {
                return Transform(position, transform);
            };
        }


        internal static TilePosition Transform(TilePosition position, ChunkTransform transform)
        {
            Func<TilePosition, TilePosition> rotate = p => RotateClockwiseAround(p, transform.ChunkCenter);
            Func<TilePosition, TilePosition> mirror = p => YMirrorOver(p, transform.ChunkCenter);

            return
                Take(position)
                .IntoWith(OffsetAround, transform.ChunkCorner)
                .RepeatFor(rotate, transform.RotationCount)
                .DoIf(mirror, transform.IsMirrored);
        }

        internal static TilePosition OffsetAround(TilePosition position, TilePosition chunkCorner)
        {
            return new TilePosition(
                chunkCorner.X + position.X,
                chunkCorner.Y + position.Y);
        }

        internal static TilePosition YMirrorOver(TilePosition position, TilePosition chunkCenter)
        {
            var yDiff = chunkCenter.Y - position.Y;

            return new TilePosition(position.X, chunkCenter.Y + yDiff);
        }

        internal static TilePosition RotateClockwiseAround(TilePosition position, TilePosition chunkCenter)
        {
            var sin = -1;
            var cos = 0;

            var translated = new TilePosition(
                position.X - chunkCenter.X,
                position.Y - chunkCenter.Y);

            var rotated = new TilePosition(
                translated.X * cos - translated.Y * sin,
                translated.Y * cos + translated.X * sin);

            return new TilePosition(
                rotated.X + chunkCenter.X,
                rotated.Y + chunkCenter.Y);
        }

    }

}