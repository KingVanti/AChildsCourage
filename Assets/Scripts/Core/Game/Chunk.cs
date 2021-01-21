using System;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using UnityEngine;
using static UnityEngine.Mathf;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game
{

    public readonly struct Chunk
    {

        public const int ChunkSize = 21;
        private const int MaxChunkCoord = ChunkSize - 1;
        internal const int ChunkExtent = MaxChunkCoord / 2;


        internal static readonly Chunk originChunk = new Chunk(0, 0);
        internal static readonly TileOffset topCornerOffset = new TileOffset(MaxChunkCoord, MaxChunkCoord);
        private static readonly TileOffset chunkCenterTileOffset = new TileOffset(ChunkExtent, ChunkExtent);


        internal static TilePosition GetCenter(Chunk chunk) =>
            chunk.Map(GetCorner)
                    .Map(OffsetBy, chunkCenterTileOffset);

        internal static Vector2 GetExactCenter(Chunk chunk) =>
            chunk.Map(GetCenter)
                 .Map(TilePosition.GetCenter);

        internal static TilePosition GetCorner(Chunk chunk) =>
            new TilePosition(chunk.X * ChunkSize,
                             chunk.Y * ChunkSize);

        internal static float GetDistanceToOrigin(Chunk chunk) =>
            new Vector2(chunk.X, chunk.Y).magnitude;

        internal static Chunk GetAdjacentChunk(PassageDirection direction, Chunk chunk)
        {
            switch (direction)
            {
                case PassageDirection.North: return new Chunk(chunk.X, chunk.Y + 1);
                case PassageDirection.East: return new Chunk(chunk.X + 1, chunk.Y);
                case PassageDirection.South: return new Chunk(chunk.X, chunk.Y - 1);
                case PassageDirection.West: return new Chunk(chunk.X - 1, chunk.Y);
                default: throw new Exception("Invalid direction!");
            }
        }

        internal static ChunkCollection GetAdjacentChunks(Chunk chunk) =>
            new ChunkCollection(chunk.Map(GetAdjacentChunk, PassageDirection.North),
                                chunk.Map(GetAdjacentChunk, PassageDirection.East),
                                chunk.Map(GetAdjacentChunk, PassageDirection.South),
                                chunk.Map(GetAdjacentChunk, PassageDirection.West));

        internal static ChunkCollection GetDiagonalAdjacentChunks(Chunk chunk) =>
            new ChunkCollection(new Chunk(chunk.X + 1, chunk.Y + 1),
                                new Chunk(chunk.X + 1, chunk.Y - 1),
                                new Chunk(chunk.X - 1, chunk.Y - 1),
                                new Chunk(chunk.X - 1, chunk.Y + 1));

        internal static Chunk Absolute(Chunk chunk) =>
            new Chunk(Abs(chunk.X), Abs(chunk.Y));

        internal static IEnumerable<TilePosition> GetPositionsInChunk(Chunk chunk) =>
            chunk.Map(GetCorner)
                    .Map(corner => Grid.GenerateTiles(corner.X, corner.Y, ChunkSize, ChunkSize));


        internal int X { get; }

        internal int Y { get; }


        internal Chunk(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";

    }

}