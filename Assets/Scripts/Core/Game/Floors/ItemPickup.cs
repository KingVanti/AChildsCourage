namespace AChildsCourage.Game.Floors
{

    public readonly struct ItemPickup
    {

        #region Properties

        public TilePosition Position { get; }

        public int ItemId { get; }

        #endregion

        #region Constructors

        public ItemPickup(TilePosition position, int itemId)
        {
            Position = position;
            ItemId = itemId;
        }

        #endregion

    }

}