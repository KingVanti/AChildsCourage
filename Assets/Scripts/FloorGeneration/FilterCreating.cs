namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        private static RoomPassageFilter CreateFilterFor(this FloorPlanBuilder builder, ChunkPosition position)
        {
            var roomType = GetFilteredRoomTypeFor(builder.GetCurrentPhase());
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
            return GoalRoomCount - (builder.CountRooms() + builder.ReservedChunks.Count);
        }


        private static ChunkPassageFilter GetPassageFilterFor(this FloorPlanBuilder builder, ChunkPosition position)
        {
            PassageFilter GetPassageFilter(Passage p) => GetFilterFor(builder, position, p);

            var north = GetPassageFilter(Passage.North);
            var east = GetPassageFilter(Passage.East);
            var south = GetPassageFilter(Passage.South);
            var west = GetPassageFilter(Passage.West);

            return new ChunkPassageFilter(north, east, south, west);
        }


        private static PassageFilter GetFilterFor(this FloorPlanBuilder builder, ChunkPosition position, Passage passage)
        {
            var positionInDirection = MoveToAdjacentChunk(position, passage);

            if (builder.IsEmpty(positionInDirection))
                return PassageFilter.Open;

            var roomAtPosition = builder.RoomsByChunks[positionInDirection];
            var hasPassage = roomAtPosition.Passages.Has(passage.Invert());

            return hasPassage ? PassageFilter.Passage : PassageFilter.NoPassage;
        }

    }

}