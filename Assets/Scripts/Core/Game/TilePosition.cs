using System;
using System.Numerics;

namespace AChildsCourage.Game
{

    public static class MTilePosition
    {

        internal static Func<TilePosition, float> GetDistanceFromOrigin =>
            position =>
                new Vector2(position.X, position.Y).Length();


        internal static Func<TilePosition, TilePosition, float> GetDistanceBetween =>
            (p1, p2) =>
                Vector2.Distance(new Vector2(p1.X, p1.Y),
                                 new Vector2(p2.X, p2.Y));


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


            public static TilePosition operator +(TilePosition position, TileOffset offset) =>
                new TilePosition(position.X + offset.X,
                                 position.Y + offset.Y);

        }

    }

}