using System;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    public static class MChunkPosition
    {

        public const int ChunkSize = 21;
        private const int ChunkExtent = (ChunkSize - 1) / 2;

        private static TileOffset ChunkCenterTileOffset { get; } = new TileOffset(ChunkExtent, ChunkExtent);

        
        public static TilePosition GetCenter(ChunkPosition position) =>
            position
                .Map(GetCorner)
                .Map(OffsetBy, ChunkCenterTileOffset);

        internal static TilePosition GetCorner(ChunkPosition position) =>
            new TilePosition(position.X * ChunkSize,
                             position.Y * ChunkSize);

        internal static float GetChunkDistanceToOrigin(ChunkPosition position) =>
            new Vector2(position.X, position.Y).magnitude;

        internal static ChunkPosition GetAdjacentChunk(ChunkPosition position, PassageDirection direction)
        {
            switch (direction)
            {
                case PassageDirection.North: return new ChunkPosition(position.X, position.Y + 1);
                case PassageDirection.East: return new ChunkPosition(position.X + 1, position.Y);
                case PassageDirection.South: return new ChunkPosition(position.X, position.Y - 1);
                case PassageDirection.West: return new ChunkPosition(position.X - 1, position.Y);
                default: throw new Exception("Invalid direction!");
            }
        }

        internal static IEnumerable<ChunkPosition> GetAdjacentChunks(ChunkPosition position)
        {
            yield return GetAdjacentChunk(position, PassageDirection.North);
            yield return GetAdjacentChunk(position, PassageDirection.East);
            yield return GetAdjacentChunk(position, PassageDirection.South);
            yield return GetAdjacentChunk(position, PassageDirection.West);
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