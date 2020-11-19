using AChildsCourage.Game.Floors;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class FloorPlanCreating
    {

        internal static FloorPlanCreator GetDefault()
        {
            return builder =>
            {
                var roomPlanCreator = RoomPlanCreating.GetDefault(builder);

                return CreateFloorPlan(builder.RoomsByChunks.Keys, roomPlanCreator);
            };
        }


        internal static FloorPlan CreateFloorPlan(IEnumerable<ChunkPosition> chunkPositions, RoomPlanCreator roomPlanCreator) =>
            chunkPositions
            .Select(roomPlanCreator.Invoke)
            .Into(ConvertToFloorPlan);

        internal static FloorPlan ConvertToFloorPlan(IEnumerable<RoomPlan> roomPlans) =>
            new FloorPlan(roomPlans.ToArray());

    }
}