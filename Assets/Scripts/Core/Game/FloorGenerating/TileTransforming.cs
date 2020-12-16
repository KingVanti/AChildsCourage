using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.F;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MTileTransforming
        {

            internal static ChunkTransform ToChunkTransform(RoomTransform transform)
            {
                var chunkCenter = GetCenter(transform.Position);
                var chunkCorner = GetCorner(transform.Position);

                return new ChunkTransform(transform.RotationCount, transform.IsMirrored, chunkCorner, chunkCenter);
            }

            internal static TilePosition Transform(TilePosition position, ChunkTransform transform)
            {
                TilePosition Rotate(TilePosition p) => RotateClockwiseAround(p, transform.ChunkCenter);

                TilePosition Mirror(TilePosition p) => YMirrorOver(p, transform.ChunkCenter);

                return
                    Take(position)
                        .MapWith(OffsetAround, transform.ChunkCorner)
                        .For(transform.RotationCount, Rotate)
                        .DoIf(Mirror, transform.IsMirrored);
            }

            private static TilePosition OffsetAround(TilePosition position, TilePosition chunkCorner) =>
                new TilePosition(
                                 chunkCorner.X + position.X,
                                 chunkCorner.Y + position.Y);

            private static TilePosition YMirrorOver(TilePosition position, TilePosition chunkCenter)
            {
                var yDiff = chunkCenter.Y - position.Y;

                return new TilePosition(position.X, chunkCenter.Y + yDiff);
            }

            private static TilePosition RotateClockwiseAround(TilePosition position, TilePosition chunkCenter)
            {
                var translated = new TilePosition(
                                                  position.X - chunkCenter.X,
                                                  position.Y - chunkCenter.Y);

                var rotated = new TilePosition(translated.Y, -translated.X);

                return new TilePosition(
                                        rotated.X + chunkCenter.X,
                                        rotated.Y + chunkCenter.Y);
            }


            internal class ChunkTransform
            {

                internal int RotationCount { get; }

                internal bool IsMirrored { get; }

                internal TilePosition ChunkCorner { get; }

                internal TilePosition ChunkCenter { get; }


                internal ChunkTransform(int rotationCount, bool isMirrored, TilePosition chunkCorner, TilePosition chunkCenter)
                {
                    RotationCount = rotationCount;
                    IsMirrored = isMirrored;
                    ChunkCorner = chunkCorner;
                    ChunkCenter = chunkCenter;
                }

            }

        }

    }

}