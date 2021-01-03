using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedStaticObject
    {

        public static SerializedStaticObject ApplyTo(SerializedStaticObject _, TilePosition position) =>
            new SerializedStaticObject(position);
        
        
        public TilePosition Position { get; }


        public SerializedStaticObject(TilePosition position) =>
            Position = position;

    }

}