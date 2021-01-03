using AChildsCourage.Game.Floors.Courage;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedCouragePickup
    {
        
        public TilePosition Position { get; }

        public CourageVariant Variant { get; }


        public SerializedCouragePickup(TilePosition position, CourageVariant variant)
        {
            Position = position;
            Variant = variant;
        }

    }

}