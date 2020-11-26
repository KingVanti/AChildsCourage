using AChildsCourage.Game.Monsters.Navigation;

namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public readonly struct AOIMarkerData
    {

        public TilePosition Position { get; }

        public int Index { get; }


        public AOIMarkerData(TilePosition position, int index)
        {
            Position = position;
            Index = index;
        }

    }

}
