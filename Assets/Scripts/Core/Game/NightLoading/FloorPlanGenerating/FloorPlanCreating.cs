using AChildsCourage.Game.Floors;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.NightLoading
{

    internal static class FloorPlanCreating
    {

        internal static FloorPlanCreator GetDefault()
        {
            return floorPlan =>
            {
                var roomPlanCreator = RoomPlanCreating.GetDefault(floorPlan);

                return CreateFloorPlan(floorPlan.RoomsByChunks.Keys, roomPlanCreator);
            };
        }


        internal static FloorPlan CreateFloorPlan(IEnumerable<ChunkPosition> chunkPositions, RoomPlanCreator roomPlanCreator) =>
            chunkPositions
            .Select(roomPlanCreator.Invoke)
            .Map(ConvertToFloorPlan);

        internal static FloorPlan ConvertToFloorPlan(IEnumerable<RoomPlan> roomPlans) =>
            new FloorPlan(roomPlans.ToArray());

    }
}