using AChildsCourage.Game.Monsters.Navigation;

namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public readonly struct AOIMarkerData
    {

        public TilePosition Position { get; }

        public AOIIndex Index { get; }


        public AOIMarkerData(TilePosition position, AOIIndex index)
        {
            Position = position;
            Index = index;
        }

    }

}
