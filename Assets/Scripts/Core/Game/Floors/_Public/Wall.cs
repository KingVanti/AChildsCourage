namespace AChildsCourage.Game.Floors
{

    public readonly struct Wall
    {

        #region Properties

        public TilePosition Position { get; }

        public WallType Type { get; }

        #endregion

        #region Constructors

        public Wall(TilePosition position, WallType type)
        {
            Position = position;
            Type = type;
        }

        #endregion

    }

}