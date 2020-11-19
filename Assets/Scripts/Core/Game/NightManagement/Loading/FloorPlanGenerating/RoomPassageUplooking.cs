using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomPassageUplooking
    {

        internal static RoomPassageLookup GetDefault(FloorPlanBuilder builder)
        {
            return position => RoomPassageUplooking.Lookup(builder, position);
        }


        internal static RoomPassages Lookup(FloorPlanBuilder builder, ChunkPosition position)
        {
            return builder.RoomsByChunks[position];
        }

    }

}