namespace AChildsCourage.Game
{

    public readonly struct TileOffset
    {
       
        #region Properties

        public int X { get; }

        public int Y { get; }

        #endregion

        #region Constructors

        public TileOffset(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

    }

}