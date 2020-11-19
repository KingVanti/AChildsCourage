namespace AChildsCourage.Game.Floors
{

    public readonly struct AOIMarker
    {

        #region Properties

        internal TilePosition Position { get; }

        internal int Index { get; }

        internal float WeightMultiplier { get; }

        #endregion

        #region Constructors

        internal AOIMarker(TilePosition position, int index, float weightMultiplier)
        {
            Position = position;
            Index = index;
            WeightMultiplier = weightMultiplier;
        }

        #endregion

    }

}