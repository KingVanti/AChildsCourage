using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct StaticObjectData
    {

        public TilePosition Position { get; }


        public StaticObjectData(TilePosition position) => Position = position;

    }

}