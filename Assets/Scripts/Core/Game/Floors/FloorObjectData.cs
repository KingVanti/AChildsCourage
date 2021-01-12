using AChildsCourage.Game.Floors.Courage;

namespace AChildsCourage.Game.Floors
{

    public abstract class FloorObjectData { }

    public class GroundTileData : FloorObjectData { }

    public class WallData : FloorObjectData
    {

        public WallType Type { get; }

        public WallData(WallType type) =>
            Type = type;

    }

    public class CouragePickupData : FloorObjectData
    {

        public CourageVariant Variant { get; }

        public CouragePickupData(CourageVariant variant) =>
            Variant = variant;

    }

    public class StaticObjectData : FloorObjectData { }

    public class RuneData : FloorObjectData { }
    
    public class PortalData : FloorObjectData { }

}