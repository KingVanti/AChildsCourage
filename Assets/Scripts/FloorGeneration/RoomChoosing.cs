namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGenerationModule
    {

        private static RoomPassages ChooseNextRoom(ChunkPosition chunkPosition, FloorPlanBuilder builder, IRoomPassagesRepository roomPassagesRepository, IRNG rng)
        {
            var filter = builder.CreateFilterFor(chunkPosition);
            var filteredRooms = roomPassagesRepository.GetRooms(filter);

            return ChooseRoomFrom(filteredRooms, rng);
        }

        private static RoomPassages ChooseRoomFrom(FilteredRoomPassages roomPassages, IRNG rng)
        {
            return roomPassages.GetRandom(rng);
        }

    }

}