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

        public static TilePosition operator +(TilePosition position, TileOffset offset)
        {
            return new TilePosition(
                position.X + offset.X,
                position.Y + offset.Y);
        }

        #endregion

    }

}