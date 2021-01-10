namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedPortal
    {

        public TilePosition Position { get; }


        public SerializedPortal(TilePosition position) =>
            Position = position;

    }

}