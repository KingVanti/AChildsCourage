namespace AChildsCourage.Game.Floors
{

    public readonly struct DataTile
    {
       
        #region Properties

        public TilePosition Position { get; }

        public DataTileType Type { get; }

        #endregion

        #region Constructors

        public DataTile(TilePosition position, DataTileType type)
        {
            Position = position;
            Type = type;
        }

        #endregion

    }

}