using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomAdding
    {

        internal static RoomAdder GetDefault(IRoomPassagesRepository roomPassagesRepository, IRNG rng)
        {
            return floorPlan =>
            {
                var chunkChooser = ChunkChoosing.GetDefault(rng);
                var roomChooser = RoomChoosing.GetDefault(roomPassagesRepository, rng);
                var roomPlacer = RoomPlacing.GetDefault(floorPlan);

                AddRoomTo(floorPlan, chunkChooser, roomChooser, roomPlacer);
                return floorPlan;
            };
        }


        internal static void AddRoomTo(FloorPlanInProgress floorPlan, ChunkChooser chunkCooser, RoomChooser roomChooser, RoomPlacer roomPlacer)
        {
            var chunk = chunkCooser(floorPlan);
            var room = roomChooser(floorPlan, chunk);

            roomPlacer.Invoke(chunk, room);
        }

    }

}