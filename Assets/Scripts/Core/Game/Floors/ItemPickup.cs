using AChildsCourage.Game.Items;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct ItemPickup
    {

        #region Properties

        public TilePosition Position { get; }

        public ItemId ItemId { get; }

        #endregion

        #region Constructors

        public ItemPickup(TilePosition position, ItemId itemId)
        {
            Position = position;
            ItemId = itemId;
        }

        #endregion

    }

}