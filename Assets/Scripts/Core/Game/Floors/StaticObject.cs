using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct StaticObject
    {

        public TilePosition Position { get; }


        public StaticObject(TilePosition position) => 
            Position = position;

    }

}