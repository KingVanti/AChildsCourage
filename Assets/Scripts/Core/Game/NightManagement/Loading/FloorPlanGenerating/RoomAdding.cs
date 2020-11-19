using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomAdding
    {

        internal static RoomAdder GetDefault(IRoomPassagesRepository roomPassagesRepository, IRNG rng)
        {
            return builder =>
            {
                var chunkChooser = ChunkChoosing.GetDefault(rng);
                var roomChooser = RoomChoosing.GetDefault(roomPassagesRepository, rng);
                var roomPlacer = RoomPlacing.GetDefault(builder);

                AddRoomTo(builder, chunkChooser, roomChooser, roomPlacer);
                return builder;
            };
        }


        internal static void AddRoomTo(FloorPlanBuilder builder, ChunkChooser chunkCooser, RoomChooser roomChooser, RoomPlacer roomPlacer)
        {
            var chunk = chunkCooser(builder);
            var room = roomChooser(builder, chunk);

            roomPlacer.Invoke(chunk, room);
        }

    }

}