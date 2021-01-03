using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;
using static UnityEngine.Mathf;

namespace AChildsCourage.Game
{

    public static class MChunkPosition
    {

        public const int ChunkSize = 21;
        public const int MaxChunkCoord = ChunkSize - 1;
        public const int ChunkExtent = MaxChunkCoord / 2;


        public static ChunkPosition OriginChunk => new ChunkPosition(0, 0);

        public static TileOffset TopCornerOffset => new TileOffset(MaxChunkCoord, MaxChunkCoord);


        private static TileOffset ChunkCenterTileOffset { get; } = new TileOffset(ChunkExtent, ChunkExtent);


        public static TilePosition GetCenter(ChunkPosition position) =>
            position
                .Map(GetCorner)
                .Map(OffsetBy, ChunkCenterTileOffset);

        internal static TilePosition GetCorner(ChunkPosition position) =>
            new TilePosition(position.X * ChunkSize,
                             position.Y * ChunkSize);

        internal static float GetDistanceToOrigin(ChunkPosition position) =>
            new Vector2(position.X, position.Y).magnitude;

        internal static ChunkPosition GetAdjacentChunk(PassageDirection direction, ChunkPosition position)
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
            yield return position.Map(GetAdjacentChunk, PassageDirection.North);
            yield return position.Map(GetAdjacentChunk, PassageDirection.East);
            yield return position.Map(GetAdjacentChunk, PassageDirection.South);
            yield return position.Map(GetAdjacentChunk, PassageDirection.West);
        }

        internal static IEnumerable<ChunkPosition> GetDiagonalAdjacentChunks(ChunkPosition position)
        {
            yield return new ChunkPosition(position.X + 1, position.Y + 1);
            yield return new ChunkPosition(position.X + 1, position.Y - 1);
            yield return new ChunkPosition(position.X - 1, position.Y - 1);
            yield return new ChunkPosition(position.X - 1, position.Y + 1);
        }

        public static ChunkPosition GetLowerLeft(IEnumerable<ChunkPosition> positions) =>
            positions
                .Map(GetBounds)
                .Map(b => new ChunkPosition(b.MinX, b.MinY));

        public static ChunkPosition Absolute(ChunkPosition position) =>
            new ChunkPosition(Abs(position.X), Abs(position.Y));

        public static (int MinX, int MinY, int MaxX, int MaxY) GetBounds(IEnumerable<ChunkPosition> positions)
        {
            var chunkPositions = positions as ChunkPosition[] ?? positions.ToArray();
            if (!chunkPositions.Any()) return (0, 0, 0, 0);
            return (chunkPositions.Min(p => p.X),
                    chunkPositions.Min(p => p.Y),
                    chunkPositions.Max(p => p.X),
                    chunkPositions.Max(p => p.Y));
        }

        public static (int Width, int Height) GetDimensions(IEnumerable<ChunkPosition> positions)
        {
            var chunkPositions = positions as ChunkPosition[] ?? positions.ToArray();

            if (!chunkPositions.Any()) return (0, 0);
            var bounds = GetBounds(chunkPositions);

            var width = bounds.MaxX - bounds.MinX + 1;
            var height = bounds.MaxY - bounds.MinY + 1;

            return (width, height);
        }

        public static IEnumerable<TilePosition> GetPositionsInChunk(ChunkPosition position) =>
            position
                .Map(GetCorner)
                .Map(corner => Grid.Generate(corner.X, corner.Y, ChunkSize, ChunkSize))
                .Select(pos => new TilePosition(pos.X, pos.Y));


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