using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomPassageUplooking
    {

        internal static RoomPassageLookup GetDefault(FloorPlanInProgress floorPlan)
        {
            return position => Lookup(floorPlan, position);
        }


        internal static RoomPassages Lookup(FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            return floorPlan.RoomsByChunks[position];
        }

    }

}