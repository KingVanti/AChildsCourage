namespace AChildsCourage.Game.Floors
{

    public readonly struct AOIMarker
    {
        
        #region Properties

        public TilePosition Position { get; }

        public int Index { get; }

        public float WeightMultiplier { get; }

        #endregion

        #region Constructors

        public AOIMarker(TilePosition position, int index, float weightMultiplier)
        {
            Position = position;
            Index = index;
            WeightMultiplier = weightMultiplier;
        }

        #endregion

    }

}