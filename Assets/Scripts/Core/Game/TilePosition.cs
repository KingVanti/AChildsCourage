using System.Collections.Generic;
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

        internal static TilePosition OffsetBy(TileOffset offset, TilePosition position) =>
            offset.Map(ApplyTo, position);

        internal static Vector3Int ToVector3Int(TilePosition position) =>
            new Vector3Int(position.X, position.Y, 0);

        internal static Vector2 ToVector2(TilePosition position) =>
            new Vector2(position.X, position.Y);

        internal static Vector2 GetCenter(TilePosition position) =>
            new Vector2(position.X + TileExtent, position.Y + TileExtent);

        internal static TileOffset AsOffset(TilePosition position) =>
            new TileOffset(position.X,
                           position.Y);

        internal static Chunk GetChunk(TilePosition tilePosition) =>
            new Chunk(FloorToInt(tilePosition.X / (float) ChunkSize),
                      FloorToInt(tilePosition.Y / (float) ChunkSize));

        internal static TilePosition RotateAround(TilePosition pivot, TilePosition position)
        {
            var translated = new TilePosition(position.X - pivot.X,
                                              position.Y - pivot.Y);
            var rotated = new TilePosition(translated.Y, -translated.X);

            return new TilePosition(rotated.X + pivot.X,
                                    rotated.Y + pivot.Y);
        }

        internal static TilePosition YMirrorOver(TilePosition pivot, TilePosition position)
        {
            var yDiff = pivot.Y - position.Y;

            return new TilePosition(position.X, pivot.Y + yDiff);
        }

        internal static IntBounds GetBounds(IReadOnlyCollection<TilePosition> positions) =>
            new IntBounds(positions.Min(p => p.X),
                          positions.Min(p => p.Y),
                          positions.Max(p => p.X),
                          positions.Max(p => p.Y));


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