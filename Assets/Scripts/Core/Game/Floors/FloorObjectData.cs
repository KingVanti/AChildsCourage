using AChildsCourage.Game.Floors.Courage;

namespace AChildsCourage.Game.Floors
{

    internal abstract class FloorObjectData { }

    internal class GroundTileData : FloorObjectData { }

    internal class WallData : FloorObjectData
    {

        internal WallType Type { get; }

        internal WallData(WallType type) =>
            Type = type;

    }

    internal class CouragePickupData : FloorObjectData
    {

        internal CourageVariant Variant { get; }

        internal CouragePickupData(CourageVariant variant) =>
            Variant = variant;

    }

    internal class StaticObjectData : FloorObjectData { }

    internal class RuneData : FloorObjectData { }

}