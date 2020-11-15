using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGenerationModule
    {

        internal const int GoalRoomCount = 15;


        internal class FloorPlanBuilder
        {

            internal Dictionary<ChunkPosition, RoomPassages> RoomsByChunks { get; } = new Dictionary<ChunkPosition, RoomPassages>();

            internal List<ChunkPosition> ReservedChunks { get; } = new List<ChunkPosition>();

        }


        internal static bool NeedsMoreRooms(this FloorPlanBuilder builder)
        {
            return builder.CountRooms() < GoalRoomCount;
        }


        internal static FloorPlan GetFloorPlan(this FloorPlanBuilder builder)
        {
            var roomsInChunks =
                builder.RoomsByChunks
                .Select(kvp => new RoomIdInChunk(kvp.Value.RoomId, kvp.Key))
                .ToArray();

            return new FloorPlan(roomsInChunks);
        }


        internal static bool HasReservedChunks(this FloorPlanBuilder builder)
        {
            return builder.ReservedChunks.Any();
        }


        internal static GenerationPhase GetCurrentPhase(this FloorPlanBuilder builder)
        {
            switch (builder.CountRooms())
            {
                case 0:
                    return GenerationPhase.StartRoom;
                case GoalRoomCount - 1:
                    return GenerationPhase.EndRoom;
                default:
                    return GenerationPhase.NormalRooms;
            }
        }


        internal static bool IsEmpty(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return !builder.RoomsByChunks.ContainsKey(position);
        }


        internal static int CountRooms(this FloorPlanBuilder builder)
        {
            return builder.RoomsByChunks.Count;
        }


        internal static bool HasReserved(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return builder.ReservedChunks.Contains(position);
        }


        internal static bool AnyPassagesLeadInto(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return builder.GetPassagesInto(position).Count > 0;
        }

        private static ChunkPassages GetPassagesInto(this FloorPlanBuilder builder, ChunkPosition position)
        {
            var hasNorth = builder.HasPassage(position, Passage.North);
            var hasEast = builder.HasPassage(position, Passage.East);
            var hasSouth = builder.HasPassage(position, Passage.South);
            var hasWest = builder.HasPassage(position, Passage.West);

            return new ChunkPassages(hasNorth, hasEast, hasSouth, hasWest);
        }

        private static bool HasPassage(this FloorPlanBuilder builder, ChunkPosition position, Passage passage)
        {
            var positionInDirection = MoveToAdjacentChunk(position, passage);

            if (builder.IsEmpty(positionInDirection))
                return false;

            var roomAtPosition = builder.RoomsByChunks[positionInDirection];
            return roomAtPosition.Passages.Has(passage.Invert());
        }

    }

}