using static UnityEngine.Mathf;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game
{

    public readonly struct TileOffset
    {

        public static TileOffset Absolute(TileOffset offset) =>
            new TileOffset(Abs(offset.X), Abs(offset.Y));

        public static TilePosition ApplyTo(TilePosition position, TileOffset offset) =>
            position.Map(OffsetBy, offset);


        public int X { get; }

        public int Y { get; }


        public TileOffset(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

}