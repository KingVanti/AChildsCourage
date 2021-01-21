using UnityEngine;

namespace AChildsCourage.Game
{

    internal readonly struct IntBounds
    {

        internal static readonly IntBounds emptyBounds = new IntBounds(0, 0, 0, 0);


        internal static Vector2 Center(IntBounds bounds) =>
            new Vector2(bounds.MinX, bounds.MinY) + new Vector2(bounds.Map(Width), bounds.Map(Height)) / 2;

        internal static int Width(IntBounds bounds) =>
            bounds.MaxX - bounds.MinX + 1;

        internal static int Height(IntBounds bounds) =>
            bounds.MaxY - bounds.MinY + 1;

        internal static (int Width, int Height) GetDimensions(IntBounds bounds) =>
            (Width(bounds), Height(bounds));

        internal static TilePosition GetMinPos(IntBounds bounds) =>
            new TilePosition(bounds.MinX, bounds.MinY);

        internal static TilePosition GetMaxPos(IntBounds bounds) =>
            new TilePosition(bounds.MaxX, bounds.MaxY);


        internal int MinX { get; }

        internal int MinY { get; }

        private int MaxX { get; }

        private int MaxY { get; }


        internal IntBounds(int minX, int minY, int maxX, int maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

    }

}