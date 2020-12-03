using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct POI
    {

        public TilePosition Position { get; }


        public POI(TilePosition position) => Position = position;

    }

}