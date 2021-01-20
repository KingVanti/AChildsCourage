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


        internal static TilePosition GetCenter(Chunk position) =>
            position.Map(GetCorner)
                    .Map(OffsetBy, chunkCenterTileOffset);

        internal static TilePosition GetCorner(Chunk position) =>
            new TilePosition(position.X * ChunkSize,
                             position.Y * ChunkSize);

        internal static float GetDistanceToOrigin(Chunk position) =>
            new Vector2(position.X, position.Y).magnitude;

        internal static Chunk GetAdjacentChunk(PassageDirection direction, Chunk position)
        {
            switch (direction)
            {
                case PassageDirection.North: return new Chunk(position.X, position.Y + 1);
                case PassageDirection.East: return new Chunk(position.X + 1, position.Y);
                case PassageDirection.South: return new Chunk(position.X, position.Y - 1);
                case PassageDirection.West: return new Chunk(position.X - 1, position.Y);
                default: throw new Exception("Invalid direction!");
            }
        }

        internal static ChunkCollection GetAdjacentChunks(Chunk position) =>
            new ChunkCollection(position.Map(GetAdjacentChunk, PassageDirection.North),
                                position.Map(GetAdjacentChunk, PassageDirection.East),
                                position.Map(GetAdjacentChunk, PassageDirection.South),
                                position.Map(GetAdjacentChunk, PassageDirection.West));

        internal static ChunkCollection GetDiagonalAdjacentChunks(Chunk position) =>
            new ChunkCollection(new Chunk(position.X + 1, position.Y + 1),
                                new Chunk(position.X + 1, position.Y - 1),
                                new Chunk(position.X - 1, position.Y - 1),
                                new Chunk(position.X - 1, position.Y + 1));

        internal static Chunk Absolute(Chunk position) =>
            new Chunk(Abs(position.X), Abs(position.Y));

        internal static IEnumerable<TilePosition> GetPositionsInChunk(Chunk position) =>
            position.Map(GetCorner)
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