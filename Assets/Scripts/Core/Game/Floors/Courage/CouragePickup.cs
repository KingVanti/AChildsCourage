using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.Courage
{

    public readonly struct CouragePickup
    {

        public TilePosition Position { get; }

        public CourageVariant Variant { get; }


        public CouragePickup(TilePosition position, CourageVariant variant)
        {
            Position = position;
            Variant = variant;
        }

    }

}