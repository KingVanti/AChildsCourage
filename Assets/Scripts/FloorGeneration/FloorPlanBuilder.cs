using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        internal const int GoalRoomCount = 15;


        internal static bool NeedsMoreRooms(this FloorPlanBuilder builder)
        {
            return builder.CountRooms() < GoalRoomCount;
        }


        internal static FloorPlan GetFloorPlan(this FloorPlanBuilder builder)
        {
            return builder.RoomsByChunks.GetFloorPlan();
        }

        internal static FloorPlan GetFloorPlan(this IDictionary<ChunkPosition, RoomPassages> roomsByChunks)
        {
            RoomPlan GetRoomAt(ChunkPosition position)
            {
                var passages = roomsByChunks[position];
                var transform = new RoomTransform(position, passages.IsMirrored, passages.RotationCount);

                return new RoomPlan(passages.RoomId, transform);
            };

            var roomsInChunks =
                roomsByChunks
                .Keys
                .Select(GetRoomAt)
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
            var hasNorth = builder.HasPassage(position, PassageDirection.North);
            var hasEast = builder.HasPassage(position, PassageDirection.East);
            var hasSouth = builder.HasPassage(position, PassageDirection.South);
            var hasWest = builder.HasPassage(position, PassageDirection.West);

            return new ChunkPassages(hasNorth, hasEast, hasSouth, hasWest);
        }

        private static bool HasPassage(this FloorPlanBuilder builder, ChunkPosition position, PassageDirection passage)
        {
            var positionInDirection = MoveToAdjacentChunk(position, passage);

            if (builder.IsEmpty(positionInDirection))
                return false;

            var roomAtPosition = builder.RoomsByChunks[positionInDirection];
            return roomAtPosition.Passages.Has(passage.Invert());
        }

    }

}