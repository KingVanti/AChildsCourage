using AChildsCourage.Game.Floors.Courage;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct CouragePickupData
    {

        public static CouragePickupData ApplyTo(CouragePickupData pickup, TilePosition position) =>
            new CouragePickupData(position, pickup.Variant);
        
        public TilePosition Position { get; }

        public CourageVariant Variant { get; }


        public CouragePickupData(TilePosition position, CourageVariant variant)
        {
            Position = position;
            Variant = variant;
        }

    }

}