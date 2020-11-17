using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        internal static void PlaceRoom(this FloorPlanBuilder builder, ChunkPosition chunkPosition, RoomPassages roomPassages)
        {
            builder.RoomsByChunks.Add(chunkPosition, roomPassages);
            builder.ReservedChunks.Remove(chunkPosition);

            Func<ChunkPosition, bool> canReserve = p => builder.CanReserve(p);
            Action<ChunkPosition> reserve = p => builder.ReservedChunks.Add(p);

            chunkPosition.ReserveChunksAround(canReserve, reserve);
        }

        private static void ReserveChunksAround(this ChunkPosition chunkPosition, Func<ChunkPosition, bool> canReserve, Action<ChunkPosition> reserve)
        {
            chunkPosition
                .GetSurroundingPositions()
                .Where(canReserve)
                .ForEach(reserve);
        }

        internal static IEnumerable<ChunkPosition> GetSurroundingPositions(this ChunkPosition position)
        {
            yield return new ChunkPosition(position.X, position.Y + 1);
            yield return new ChunkPosition(position.X + 1, position.Y);
            yield return new ChunkPosition(position.X, position.Y - 1);
            yield return new ChunkPosition(position.X - 1, position.Y);
        }

        internal static bool CanReserve(this FloorPlanBuilder builder, ChunkPosition position)
        {
            var isReserved = builder.HasReserved(position);
            var isEmpty = builder.IsEmpty(position);
            var isConnected = builder.AnyPassagesLeadInto(position);

            return !isReserved && isEmpty && isConnected;
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

            var roomAtPosition = builder.GetPassagesAt(positionInDirection);
            return roomAtPosition.Passages.Has(passage.Invert());
        }

    }

}
