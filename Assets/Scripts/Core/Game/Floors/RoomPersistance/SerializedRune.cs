namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedRune
    {

        public TilePosition Position { get; }


        public SerializedRune(TilePosition position) =>
            Position = position;

    }

}