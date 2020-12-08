using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    public static class MChunkPosition
    {

        public const int ChunkSize = 21;
        public const int ChunkExtent = (ChunkSize - 1) / 2;


        public static TilePosition GetChunkCenter(ChunkPosition position) => GetChunkCorner(position) + new TileOffset(ChunkExtent, ChunkExtent);
        
        internal static TilePosition GetChunkCorner(ChunkPosition chunkPosition) =>
            new TilePosition(chunkPosition.X * ChunkSize,
                             chunkPosition.Y * ChunkSize);

        public readonly struct ChunkPosition
        {

            public int X { get; }

            public int Y { get; }


            public ChunkPosition(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString() => $"({X}, {Y})";

        }

    }

}