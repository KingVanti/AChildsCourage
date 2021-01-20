using UnityEngine;
using static UnityEngine.Mathf;

namespace AChildsCourage.Game
{

    internal readonly struct TileOffset
    {

        internal static TileOffset Absolute(TileOffset offset) =>
            new TileOffset(Abs(offset.X), Abs(offset.Y));

        internal static TilePosition ApplyTo(TilePosition position, TileOffset offset) =>
            new TilePosition(position.X + offset.X,
                             position.Y + offset.Y);

        internal static float Magnitude(TileOffset offset) =>
            new Vector2(offset.X, offset.Y).magnitude;


        internal int X { get; }

        internal int Y { get; }


        internal TileOffset(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

}