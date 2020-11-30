using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using static AChildsCourage.F;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorPlanGenerating
    {

        internal static RoomInChunk ChooseNextRoom(ChunkPosition chunkPosition, FloorPlanInProgress floorPlan, IEnumerable<RoomPassages> allPassages, CreateRNG createRng)
        {
            return chunkPosition
                   .CreateFilter(floorPlan)
                   .FilterPassagesMatching(allPassages)
                   .ChooseRandom(createRng)
                   .Map(room => new RoomInChunk(room, chunkPosition));
        }

        private static RoomPassages ChooseRandom(this FilteredRoomPassages roomPassages, CreateRNG createRng)
        {
            return roomPassages.GetRandom(createRng);
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
            return (RoomType) (int) buildingPhase;
        }

        internal static int GetRemainingRoomCount(this FloorPlanInProgress floorPlan)
        {
            return GoalRoomCount - (CountRooms(floorPlan) + floorPlan.ReservedChunks.Count);
        }

        private static ChunkPassageFilter GetPassageFilterFor(this FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            PassageFilter GetPassageFilter(PassageDirection p)
            {
                return GetFilterFor(floorPlan, position, p);
            }

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