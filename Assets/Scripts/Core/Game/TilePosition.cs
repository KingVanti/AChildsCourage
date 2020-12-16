using System;
using UnityEngine;
using static UnityEngine.Mathf;

namespace AChildsCourage.Game
{

    public static class MTilePosition
    {

        internal static Func<TilePosition, float> GetDistanceFromOrigin =>
            position =>
                new Vector2(position.X, position.Y).magnitude;


        internal static Func<TilePosition, TilePosition, float> GetDistanceBetween =>
            (p1, p2) =>
                Vector2.Distance(new Vector2(p1.X, p1.Y),
                                 new Vector2(p2.X, p2.Y));

        public static Func<TilePosition, TileOffset, TilePosition> OffsetTilePosition =>
            (position, offset) =>
                new TilePosition(position.X + offset.X,
                                 position.Y + offset.Y);

        public static Func<TilePosition, Vector3Int> ToVector3Int =>
            tilePosition =>
                new Vector3Int(tilePosition.X,
                               tilePosition.Y,
                               0);

        public static Func<TilePosition, Vector2> ToVector2 =>
            tilePosition =>
                new Vector2(tilePosition.X,
                            tilePosition.Y);

        public static Func<TilePosition, Vector2> GetTileCenter =>
            tilePosition =>
                new Vector2(tilePosition.X + 0.5f,
                            tilePosition.Y + 0.5f);

        public static Func<Vector2, TilePosition> ToTile =>
            vector =>
                new TilePosition(FloorToInt(vector.x),
                                 FloorToInt(vector.y));


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