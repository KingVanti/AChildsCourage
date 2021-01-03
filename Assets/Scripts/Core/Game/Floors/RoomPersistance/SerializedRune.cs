using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedRune
    {

        public static SerializedRune ApplyTo(SerializedRune _, TilePosition position) =>
            new SerializedRune(position);

        
        public TilePosition Position { get; }


        public SerializedRune(TilePosition position) => 
            Position = position;

    }

}