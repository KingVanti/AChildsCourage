using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static UnityEngine.Mathf;
using static AChildsCourage.Game.Chunk;
using static AChildsCourage.Game.TileOffset;

namespace AChildsCourage.Game
{

    public readonly struct TilePosition
    {

        private const float TileSize = 1f;
        private const float TileExtent = TileSize / 2f;
        
        
        internal static float DistanceTo(TilePosition p1, TilePosition p2) =>
            Vector2.Distance(new Vector2(p1.X, p1.Y), new Vector2(p2.X, p2.Y));

        public static TilePosition OffsetBy(TileOffset offset, TilePosition position) =>
            offset.Map(ApplyTo, position);

        public static Vector3Int ToVector3Int(TilePosition position) =>
            new Vector3Int(position.X, position.Y, 0);

        public static Vector2 ToVector2(TilePosition position) =>
            new Vector2(position.X, position.Y);

        public static Vector2 GetCenter(TilePosition position) =>
            new Vector2(position.X + TileExtent, position.Y + TileExtent);

        public static TilePosition ToTile(Vector2 vector) =>
            new TilePosition(FloorToInt(vector.x),
                             FloorToInt(vector.y));

        public static IEnumerable<TilePosition> FindPositionsInRadius(TilePosition center, float radius) =>
            GeneratePositionsInRadius(GetCenter(center), radius)
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

        public static Chunk GetChunk(TilePosition tilePosition) =>
            new Chunk(FloorToInt(tilePosition.X / (float) ChunkSize),
                              FloorToInt(tilePosition.Y / (float) ChunkSize));

        public static TilePosition RotateAround(TilePosition pivot, TilePosition position)
        {
            var translated = new TilePosition(position.X - pivot.X,
                                              position.Y - pivot.Y);
            var rotated = new TilePosition(translated.Y, -translated.X);

            return new TilePosition(rotated.X + pivot.X,
                                    rotated.Y + pivot.Y);
        }

        public static TilePosition YMirrorOver(TilePosition pivot, TilePosition position)
        {
            var yDiff = pivot.Y - position.Y;

            return new TilePosition(position.X, pivot.Y + yDiff);
        }

        public int X { get; }

        public int Y { get; }


        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";

    }

}