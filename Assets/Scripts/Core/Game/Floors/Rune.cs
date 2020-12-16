using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct Rune
    {

        public TilePosition Position { get; }


        public Rune(TilePosition position) => Position = position;

    }

}