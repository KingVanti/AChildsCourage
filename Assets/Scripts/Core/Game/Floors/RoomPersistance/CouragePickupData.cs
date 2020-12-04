using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct CouragePickupData
    {

        public TilePosition Position { get; }

        public CourageVariant Variant { get; }


        public CouragePickupData(TilePosition position, CourageVariant variant)
        {
            Position = position;
            Variant = variant;
        }

    }

}