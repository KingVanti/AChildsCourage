namespace AChildsCourage.Game.Floors
{

    public readonly struct GroundTile
    {

        #region Properties

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        public GroundTile(TilePosition position)
        {
            Position = position;
        }

        #endregion

    }

}