using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        internal const int GoalRoomCount = 15;

 
        internal static FloorPlan GetFloorPlan(this FloorPlanBuilder builder)
        {
            return builder.RoomsByChunks.ToFloorPlan();
        }

       
        internal static bool HasReservedChunks(this FloorPlanBuilder builder)
        {
            return builder.ReservedChunks.Any();
        }


        internal static bool IsEmpty(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return !builder.RoomsByChunks.ContainsKey(position);
        }


        internal static int CountRooms(this FloorPlanBuilder builder)
        {
            return builder.RoomsByChunks.Count;
        }


        internal static bool HasReserved(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return builder.ReservedChunks.Contains(position);
        }


        private static RoomPassages GetPassagesAt(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return builder.RoomsByChunks[position];
        }

    }

}