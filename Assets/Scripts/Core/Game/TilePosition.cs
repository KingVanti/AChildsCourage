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


        public override bool Equals(object obj)
        {
            return obj is TilePosition position &&
                   X == position.X &&
                   Y == position.Y;
        }


        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
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