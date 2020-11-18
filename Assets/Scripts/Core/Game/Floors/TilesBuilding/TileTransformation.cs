using System;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static GroundTile TransformGroundTile(this TilePositionTransformer transformer, GroundTile groundTile)
        {
            var newPosition = transformer.Transform(groundTile.Position);

            return groundTile.With(newPosition);
        }

        internal static GroundTile With(this GroundTile groundTile, TilePosition position)
        {
            return new GroundTile(position, groundTile.DistanceToWall, groundTile.AOIIndex);
        }


        private static TilePosition Transform(this TilePositionTransformer transformer, TilePosition position)
        {
            Func<TilePosition, TilePosition> rotate = p => RotateClockwiseAround(p, transformer.ChunkCenter);

            Func<TilePosition, TilePosition> mirror = p => YMirrorOver(p, transformer.ChunkCenter);

            return
                position
                .OffsetAround(transformer.ChunkCorner)
                .RepeatFeed(rotate, transformer.RotationCount)
                .DoIf(mirror, transformer.IsMirrored);
        }

        internal static TilePosition OffsetAround(this TilePosition position, TilePosition chunkCenter)
        {
            return new TilePosition(
                chunkCenter.X + position.X,
                chunkCenter.Y + position.Y);
        }

        internal static TilePosition YMirrorOver(this TilePosition position, TilePosition chunkCenter)
        {
            var yDiff = chunkCenter.Y - position.Y;

            return new TilePosition(position.X, chunkCenter.Y + yDiff);
        }

        internal static TilePosition RotateClockwiseAround(this TilePosition position, TilePosition chunkCenter)
        {
            var sin = Math.Sin(270);
            var cos = Math.Cos(270);

            var translated = new TilePosition(
                position.X - chunkCenter.X,
                position.Y - chunkCenter.Y);

            var rotated = new TilePosition(
                (int)(translated.X * cos - translated.Y * sin),
                (int)(translated.X * sin - translated.Y * cos));

            return new TilePosition(
                rotated.X + chunkCenter.X,
                rotated.Y + chunkCenter.Y);
        }

    }

}