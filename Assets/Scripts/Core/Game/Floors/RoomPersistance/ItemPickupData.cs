namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public readonly struct ItemPickupData
    {

        public TilePosition Position { get; }


        public ItemPickupData(TilePosition position)
        {
            Position = position;
        }

    }

}