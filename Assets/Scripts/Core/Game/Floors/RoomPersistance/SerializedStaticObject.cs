namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedStaticObject
    {
        
        public TilePosition Position { get; }


        public SerializedStaticObject(TilePosition position) =>
            Position = position;

    }

}