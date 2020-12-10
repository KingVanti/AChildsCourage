using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct RuneData
    {

        public TilePosition Position { get; }


        public RuneData(TilePosition position) => Position = position;

    }

}