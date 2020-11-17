namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        private static RoomPassages ChooseNextRoom(ChunkPosition chunkPosition, FloorPlanBuilder builder, IRoomPassagesRepository roomPassagesRepository, IRNG rng)
        {
            return chunkPosition
                .CreateFilter(builder)
                .FilterRoomsIn(roomPassagesRepository)
                .ChooseRandom(rng);
        }

        private static FilteredRoomPassages FilterRoomsIn(this RoomPassageFilter filter, IRoomPassagesRepository roomPassagesRepository)
        {
            return roomPassagesRepository.GetRooms(filter);
        }

        private static RoomPassages ChooseRandom(this FilteredRoomPassages roomPassages, IRNG rng)
        {
            return roomPassages.GetRandom(rng);
        }

    }

}