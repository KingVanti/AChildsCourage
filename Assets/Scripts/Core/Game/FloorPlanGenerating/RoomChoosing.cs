using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using static AChildsCourage.MFunctional;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorPlanGenerating
    {

        internal static RoomInChunk ChooseNextRoom(ChunkPosition chunkPosition, FloorPlanInProgress floorPlan, IEnumerable<RoomPassages> allPassages, CreateRng createRng)
        {
            return chunkPosition
                   .CreateFilter(floorPlan)
                   .FilterPassagesMatching(allPassages)
                   .ChooseRandom(createRng)
                   .Map(room => new RoomInChunk(room, chunkPosition));
        }

        private static RoomPassages ChooseRandom(this FilteredRoomPassages roomPassages, CreateRng createRng) => roomPassages.GetRandom(createRng);

        private static RoomPassageFilter CreateFilter(this ChunkPosition position, FloorPlanInProgress floorPlan)
        {
            var phase = Take(floorPlan)
                        .Map(CountRooms)
                        .Map(GetCurrentPhase);

            var roomType = GetFilteredRoomTypeFor(phase);
            var remainingRooms = floorPlan.GetRemainingRoomCount();
            var passageFilter = floorPlan.GetPassageFilterFor(position);

            return new RoomPassageFilter(roomType, remainingRooms, passageFilter);
        }

        internal static RoomType GetFilteredRoomTypeFor(GenerationPhase buildingPhase) => (RoomType) (int) buildingPhase;

        internal static int GetRemainingRoomCount(this FloorPlanInProgress floorPlan) => GoalRoomCount - (CountRooms(floorPlan) + floorPlan.ReservedChunks.Count);

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