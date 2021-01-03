using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct StaticObjectData
    {

        public static StaticObjectData ApplyTo(StaticObjectData _, TilePosition position) =>
            new StaticObjectData(position);
        
        
        public TilePosition Position { get; }


        public StaticObjectData(TilePosition position) =>
            Position = position;

    }

}