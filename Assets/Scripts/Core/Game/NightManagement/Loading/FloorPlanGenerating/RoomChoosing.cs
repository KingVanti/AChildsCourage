using AChildsCourage.Game.Floors;
using static AChildsCourage.F;
using static AChildsCourage.Game.NightManagement.Loading.FloorGenerationUtility;
using static AChildsCourage.Game.NightManagement.Loading.FloorPlanGenerating;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomChoosing
    {

        internal static RoomChooser GetDefault(IRoomPassagesRepository roomPassagesRepository, IRNG rng)
        {
            return (builder, position) => ChooseNextRoom(position, builder, roomPassagesRepository, rng);
        }


        internal static RoomPassages ChooseNextRoom(ChunkPosition chunkPosition, FloorPlanBuilder builder, IRoomPassagesRepository roomPassagesRepository, IRNG rng)
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

        private static RoomPassageFilter CreateFilter(this ChunkPosition position, FloorPlanBuilder builder)
        {
            var phase =
                Pipe(builder)
                .Into(CountRooms)
                .Then().Into(GetCurrentPhase);

            var roomType = GetFilteredRoomTypeFor(phase);
            var remainingRooms = builder.GetRemainingRoomCount();
            var passageFilter = builder.GetPassageFilterFor(position);

            return new RoomPassageFilter(roomType, remainingRooms, passageFilter);
        }

        internal static RoomType GetFilteredRoomTypeFor(GenerationPhase buildingPhase)
        {
            return (RoomType)(int)buildingPhase;
        }

        internal static int GetRemainingRoomCount(this FloorPlanBuilder builder)
        {
            return GoalRoomCount - (CountRooms(builder) + builder.ReservedChunks.Count);
        }

        private static ChunkPassageFilter GetPassageFilterFor(this FloorPlanBuilder builder, ChunkPosition position)
        {
            PassageFilter GetPassageFilter(PassageDirection p) => GetFilterFor(builder, position, p);

            var north = GetPassageFilter(PassageDirection.North);
            var east = GetPassageFilter(PassageDirection.East);
            var south = GetPassageFilter(PassageDirection.South);
            var west = GetPassageFilter(PassageDirection.West);

            return new ChunkPassageFilter(north, east, south, west);
        }

        private static PassageFilter GetFilterFor(this FloorPlanBuilder builder, ChunkPosition position, PassageDirection direction)
        {
            var positionInDirection = MoveToAdjacentChunk(position, direction);

            if (IsEmpty(builder, positionInDirection))
                return PassageFilter.Open;

            var roomAtPosition = builder.RoomsByChunks[positionInDirection];
            var hasPassage =
                Pipe(direction)
               .Into(Invert)
               .Then().Into(roomAtPosition.Passages.Has);

            return hasPassage ? PassageFilter.Passage : PassageFilter.NoPassage;
        }

    }

}