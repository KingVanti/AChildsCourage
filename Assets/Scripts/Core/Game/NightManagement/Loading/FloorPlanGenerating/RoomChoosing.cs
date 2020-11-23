using AChildsCourage.Game.Floors;
using static AChildsCourage.F;
using static AChildsCourage.Game.NightManagement.Loading.FloorPlanGeneratingUtility;
using static AChildsCourage.Game.NightManagement.Loading.FloorPlanGenerating;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomChoosing
    {

        internal static RoomChooser GetDefault(IRoomPassagesRepository roomPassagesRepository, IRNG rng)
        {
            return (floorPlan, position) => ChooseNextRoom(position, floorPlan, roomPassagesRepository, rng);
        }


        internal static RoomPassages ChooseNextRoom(ChunkPosition chunkPosition, FloorPlanInProgress floorPlan, IRoomPassagesRepository roomPassagesRepository, IRNG rng)
        {
            return chunkPosition
                .CreateFilter(floorPlan)
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

        private static RoomPassageFilter CreateFilter(this ChunkPosition position, FloorPlanInProgress floorPlan)
        {
            var phase =
                Take(floorPlan)
                .Map(CountRooms)
                .Map(GetCurrentPhase);

            var roomType = GetFilteredRoomTypeFor(phase);
            var remainingRooms = floorPlan.GetRemainingRoomCount();
            var passageFilter = floorPlan.GetPassageFilterFor(position);

            return new RoomPassageFilter(roomType, remainingRooms, passageFilter);
        }

        internal static RoomType GetFilteredRoomTypeFor(GenerationPhase buildingPhase)
        {
            return (RoomType)(int)buildingPhase;
        }

        internal static int GetRemainingRoomCount(this FloorPlanInProgress floorPlan)
        {
            return GoalRoomCount - (CountRooms(floorPlan) + floorPlan.ReservedChunks.Count);
        }

        private static ChunkPassageFilter GetPassageFilterFor(this FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            PassageFilter GetPassageFilter(PassageDirection p) => GetFilterFor(floorPlan, position, p);

            var north = GetPassageFilter(PassageDirection.North);
            var east = GetPassageFilter(PassageDirection.East);
            var south = GetPassageFilter(PassageDirection.South);
            var west = GetPassageFilter(PassageDirection.West);

            return new ChunkPassageFilter(north, east, south, west);
        }

        private static PassageFilter GetFilterFor(this FloorPlanInProgress floorPlan, ChunkPosition position, PassageDirection direction)
        {
            var positionInDirection = MoveToAdjacentChunk(position, direction);

            if (IsEmpty(floorPlan, positionInDirection))
                return PassageFilter.Open;

            var roomAtPosition = floorPlan.RoomsByChunks[positionInDirection];
            var hasPassage =
                Take(direction)
               .Map(Invert)
               .Map(roomAtPosition.Passages.Has);

            return hasPassage ? PassageFilter.Passage : PassageFilter.NoPassage;
        }

    }

}