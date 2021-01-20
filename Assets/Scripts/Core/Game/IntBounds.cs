namespace AChildsCourage.Game
{

    public readonly struct IntBounds
    {

        public static readonly IntBounds emptyBounds = new IntBounds(0, 0, 0, 0);


        public static int Width(IntBounds bounds) =>
            bounds.MaxX - bounds.MinX + 1;

        public static int Height(IntBounds bounds) =>
            bounds.MaxY - bounds.MinY + 1;

        public static (int Width, int Height) GetDimensions(IntBounds bounds) =>
            (Width(bounds), Height(bounds));


        public int MinX { get; }

        public int MinY { get; }

        public int MaxX { get; }

        public int MaxY { get; }


        public IntBounds(int minX, int minY, int maxX, int maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }

    }

}