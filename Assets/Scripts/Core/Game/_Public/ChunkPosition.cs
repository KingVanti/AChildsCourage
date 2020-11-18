namespace AChildsCourage.Game
{

    public readonly struct ChunkPosition
    {

        #region Constants

        public const int ChunkSize = 41;
        public const int ChunkExtent = (ChunkSize - 1) / 2;

        #endregion

        #region Properties

        public int X { get; }

        public int Y { get; }

        #endregion

        #region Constructors

        public ChunkPosition(int x, int y)
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

    }

}