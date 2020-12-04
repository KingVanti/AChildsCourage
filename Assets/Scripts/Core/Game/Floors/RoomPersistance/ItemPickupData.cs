using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct ItemPickupData
    {

        public TilePosition Position { get; }


        public ItemPickupData(TilePosition position) => Position = position;

    }

}