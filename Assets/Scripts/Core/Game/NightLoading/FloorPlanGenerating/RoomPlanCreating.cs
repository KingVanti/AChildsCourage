using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightLoading
{

    internal static class RoomPlanCreating
    {

        internal static RoomPlanCreator GetDefault(FloorPlanInProgress floorPlan)
        {
            var roomPassageLookup = RoomPassageUplooking.GetDefault(floorPlan);

            return position => CreateRoomPlan(position, roomPassageLookup);
        }


        internal static RoomPlan CreateRoomPlan(ChunkPosition position, RoomPassageLookup roomPassageLookup)
        {
            var passages = roomPassageLookup(position);
            var transform = new RoomTransform(position, passages.IsMirrored, passages.RotationCount);

            return new RoomPlan(passages.RoomId, transform);
        }

    }

}