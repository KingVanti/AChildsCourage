using System;
using System.Collections.Generic;
using System.Numerics;
using static AChildsCourage.Game.Floors.MPassageDirection;
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

        internal static float GetDistanceToOrigin(ChunkPosition position) => new Vector2(position.X, position.Y).Length();

        internal static IEnumerable<ChunkPosition> GetAdjacentChunks(ChunkPosition position)
        {
            yield return GetAdjacentChunk(position, PassageDirection.North);
            yield return GetAdjacentChunk(position, PassageDirection.East);
            yield return GetAdjacentChunk(position, PassageDirection.South);
            yield return GetAdjacentChunk(position, PassageDirection.West);
        }

        internal static ChunkPosition GetAdjacentChunk(ChunkPosition position, PassageDirection direction)
        {
            switch (direction)
            {
                case PassageDirection.North: return new ChunkPosition(position.X, position.Y + 1);
                case PassageDirection.East: return new ChunkPosition(position.X + 1, position.Y);
                case PassageDirection.South: return new ChunkPosition(position.X, position.Y - 1);
                case PassageDirection.West: return new ChunkPosition(position.X - 1, position.Y);
            }

            throw new Exception("Invalid direction!");
        }

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