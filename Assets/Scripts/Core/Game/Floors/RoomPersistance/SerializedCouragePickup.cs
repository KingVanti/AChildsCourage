using AChildsCourage.Game.Floors.Courage;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedCouragePickup
    {

        public static SerializedCouragePickup ApplyTo(SerializedCouragePickup pickup, TilePosition position) =>
            new SerializedCouragePickup(position, pickup.Variant);
        
        public TilePosition Position { get; }

        public CourageVariant Variant { get; }


        public SerializedCouragePickup(TilePosition position, CourageVariant variant)
        {
            Position = position;
            Variant = variant;
        }

    }

}