namespace AChildsCourage.Game
{

    public readonly struct TilePosition
    {

        #region Properties

        public int X { get; }

        public int Y { get; }

        #endregion

        #region Constructors

        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        #endregion

        #region Operators

        public static TilePosition operator +(TilePosition t1, TilePosition t2)
        {
            return new TilePosition(
                t1.X + t2.X,
                t1.Y + t2.Y);
        }

        #endregion

    }

}