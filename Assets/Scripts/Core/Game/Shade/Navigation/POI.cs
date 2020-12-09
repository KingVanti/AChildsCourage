using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct Poi
    {

        public TilePosition Position { get; }


        public Poi(TilePosition position) => Position = position;

    }

}