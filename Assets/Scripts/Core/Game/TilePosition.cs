using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.MChunkPosition;
using static UnityEngine.Mathf;

namespace AChildsCourage.Game
{

    public static class MTilePosition
    {

        private const float TileSize = 1f;
        private const float TileExtent = TileSize / 2f;


        internal static float GetDistanceFromOrigin(TilePosition position) =>
            new Vector2(position.X, position.Y).magnitude;

        internal static float DistanceTo(TilePosition p1, TilePosition p2) =>
            Vector2.Distance(new Vector2(p1.X, p1.Y), new Vector2(p2.X, p2.Y));

        public static TilePosition OffsetBy(TileOffset offset, TilePosition position) =>
            new TilePosition(position.X + offset.X,
                             position.Y + offset.Y);

        public static TilePosition ApplyTo(TilePosition position, TileOffset offset) =>
            position.Map(OffsetBy, offset);

        public static Vector3Int ToVector3Int(TilePosition position) =>
            new Vector3Int(position.X, position.Y, 0);

        public static Vector2 ToVector2(TilePosition position) =>
            new Vector2(position.X, position.Y);

        public static Vector2 GetTileCenter(TilePosition position) =>
            new Vector2(position.X + TileExtent, position.Y + TileExtent);

        public static TilePosition ToTile(Vector2 vector) =>
            new TilePosition(FloorToInt(vector.x),
                             FloorToInt(vector.y));

        public static IEnumerable<TilePosition> FindPositionsInRadius(TilePosition center, float radius) =>
            GeneratePositionsInRadius(GetTileCenter(center), radius)
                .Select(ToTile);

        private static IEnumerable<Vector2> GeneratePositionsInRadius(Vector2 center, float radius) =>
            GeneratePositionsInSquare(center, radius)
                .Where(v => Vector2.Distance(v, center) <= radius);

        private static IEnumerable<Vector2> GeneratePositionsInSquare(Vector2 center, float extend)
        {
            for (var dX = -extend; dX <= extend; dX++)
                for (var dY = -extend; dY <= extend; dY++)
                    yield return new Vector2(center.x + dX, center.y + dY);
        }

        public static TilePosition Average(ImmutableHashSet<TilePosition> positions) =>
            new TilePosition((int) positions.Select(p => p.X).Average(),
                             (int) positions.Select(p => p.Y).Average());

        public static TileOffset AsOffset(TilePosition position) =>
            new TileOffset(position.X,
                           position.Y);

        public static TileOffset Absolute(TileOffset offset) =>
            new TileOffset(Abs(offset.X),
                           Abs(offset.Y));

        public static ChunkPosition GetChunk(TilePosition tilePosition) =>
            new ChunkPosition(FloorToInt(tilePosition.X / (float) ChunkSize),
                              FloorToInt(tilePosition.Y / (float) ChunkSize));


        public readonly struct TilePosition
        {

            public int X { get; }

            public int Y { get; }


            public TilePosition(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override string ToString() => $"({X}, {Y})";

        }

        public readonly struct TileOffset
        {

            public int X { get; }

            public int Y { get; }


            public TileOffset(int x, int y)
            {
                X = x;
                Y = y;
            }

        }

    }

}