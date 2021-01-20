﻿using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using UnityEngine;
using static UnityEngine.Mathf;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game
{

    public readonly struct Chunk
    {

        public const int ChunkSize = 21;
        public const int MaxChunkCoord = ChunkSize - 1;
        public const int ChunkExtent = MaxChunkCoord / 2;


        public static Chunk OriginChunk => new Chunk(0, 0);

        public static TileOffset TopCornerOffset => new TileOffset(MaxChunkCoord, MaxChunkCoord);


        private static TileOffset ChunkCenterTileOffset { get; } = new TileOffset(ChunkExtent, ChunkExtent);


        public static TilePosition GetCenter(Chunk position) =>
            position.Map(GetCorner)
                    .Map(OffsetBy, ChunkCenterTileOffset);

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

        internal static IEnumerable<Chunk> GetAdjacentChunks(Chunk position)
        {
            yield return position.Map(GetAdjacentChunk, PassageDirection.North);
            yield return position.Map(GetAdjacentChunk, PassageDirection.East);
            yield return position.Map(GetAdjacentChunk, PassageDirection.South);
            yield return position.Map(GetAdjacentChunk, PassageDirection.West);
        }

        internal static IEnumerable<Chunk> GetDiagonalAdjacentChunks(Chunk position)
        {
            yield return new Chunk(position.X + 1, position.Y + 1);
            yield return new Chunk(position.X + 1, position.Y - 1);
            yield return new Chunk(position.X - 1, position.Y - 1);
            yield return new Chunk(position.X - 1, position.Y + 1);
        }

        public static Chunk GetLowerLeft(IEnumerable<Chunk> positions) =>
            positions.Map(GetBounds)
                     .Map(b => new Chunk(b.MinX, b.MinY));

        public static Chunk Absolute(Chunk position) =>
            new Chunk(Abs(position.X), Abs(position.Y));

        public static (int MinX, int MinY, int MaxX, int MaxY) GetBounds(IEnumerable<Chunk> positions)
        {
            var chunks = positions as Chunk[] ?? positions.ToArray();
            if (!chunks.Any()) return (0, 0, 0, 0);
            return (chunks.Min(p => p.X),
                    chunks.Min(p => p.Y),
                    chunks.Max(p => p.X),
                    chunks.Max(p => p.Y));
        }

        public static (int Width, int Height) GetDimensions(IEnumerable<Chunk> positions)
        {
            var chunks = positions as Chunk[] ?? positions.ToArray();

            if (!chunks.Any()) return (0, 0);
            var bounds = GetBounds(chunks);

            var width = bounds.MaxX - bounds.MinX + 1;
            var height = bounds.MaxY - bounds.MinY + 1;

            return (width, height);
        }

        public static IEnumerable<TilePosition> GetPositionsInChunk(Chunk position) =>
            position.Map(GetCorner)
                    .Map(corner => Grid.Generate(corner.X, corner.Y, ChunkSize, ChunkSize))
                    .Select(pos => new TilePosition(pos.X, pos.Y));


        public int X { get; }

        public int Y { get; }


        public Chunk(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";

    }

}