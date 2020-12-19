using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Mathf;

namespace AChildsCourage.Game
{

    public static class MTilePosition
    {

        internal static Func<TilePosition, float> GetDistanceFromOrigin =>
            position =>
                new Vector2(position.X, position.Y).magnitude;


        internal static Func<TilePosition, TilePosition, float> DistanceTo =>
            (p1, p2) =>
                Vector2.Distance(new Vector2(p1.X, p1.Y), new Vector2(p2.X, p2.Y));

        public static Func<TilePosition, TileOffset, TilePosition> OffsetBy =>
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

        public static Func<TilePosition, float, IEnumerable<TilePosition>> FindPositionsInRadius =>
            (center, radius) =>
                GeneratePositionsInRadius(GetTileCenter(center), radius)
                    .Select(ToTile);


        private static Func<Vector2, float, IEnumerable<Vector2>> GeneratePositionsInRadius =>
            (center, radius) =>
                GeneratePositionsInSquare(center, radius)
                    .Where(v => Vector2.Distance(v, center) <= radius);

        private static IEnumerable<Vector2> GeneratePositionsInSquare(Vector2 center, float extend)
        {
            for (var dX = -extend; dX <= extend; dX++)
                for (var dY = -extend; dY <= extend; dY++)
                    yield return new Vector2(center.x + dX, center.y + dY);
        }

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