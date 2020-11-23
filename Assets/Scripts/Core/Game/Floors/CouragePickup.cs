namespace AChildsCourage.Game.Floors
{

    public readonly struct CouragePickup
    {
        
        #region Properties

        public TilePosition Position { get; }

        public CourageVariant Variant { get; }

        #endregion

        #region Constructors

        public CouragePickup(TilePosition position, CourageVariant variant)
        {
            Position = position;
            Variant = variant;
        }

        #endregion

    }

}