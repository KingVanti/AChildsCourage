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

    }

}