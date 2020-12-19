using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct Wall
    {

        public TilePosition Position { get; }

        public WallType Type { get; }


        public Wall(TilePosition position, WallType type)
        {
            Position = position;
            Type = type;
        }

    }

}