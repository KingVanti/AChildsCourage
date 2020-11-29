using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.F;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorPlanGenerating
    {

        internal static FloorPlanInProgress PlaceRoom(FloorPlanInProgress floorPlan, RoomInChunk roomInChunk)
        {
            floorPlan.RoomsByChunks.Add(roomInChunk.Position, roomInChunk.Room);
            floorPlan.ReservedChunks.Remove(roomInChunk.Position);

            Func<ChunkPosition, bool> canReserve = p => floorPlan.CanReserve(p);
            Action<ChunkPosition> reserve = p => floorPlan.ReservedChunks.Add(p);

            roomInChunk.Position.ReserveChunksAround(canReserve, reserve);

            return floorPlan;
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

        internal static bool CanReserve(this FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            var isReserved = floorPlan.HasReserved(position);
            var isEmpty = IsEmpty(floorPlan, position);
            var isConnected = floorPlan.AnyPassagesLeadInto(position);

            return !isReserved && isEmpty && isConnected;
        }

        internal static bool HasReserved(this FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            return floorPlan.ReservedChunks.Contains(position);
        }

        internal static bool AnyPassagesLeadInto(this FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            return floorPlan.GetPassagesInto(position)
                            .Count >
                   0;
        }

        private static ChunkPassages GetPassagesInto(this FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            var hasNorth = floorPlan.HasPassage(position, PassageDirection.North);
            var hasEast = floorPlan.HasPassage(position, PassageDirection.East);
            var hasSouth = floorPlan.HasPassage(position, PassageDirection.South);
            var hasWest = floorPlan.HasPassage(position, PassageDirection.West);

            return new ChunkPassages(hasNorth, hasEast, hasSouth, hasWest);
        }

        private static bool HasPassage(this FloorPlanInProgress floorPlan, ChunkPosition position, PassageDirection direction)
        {
            var positionInDirection = MoveToAdjacentChunk(position, direction);

            if (IsEmpty(floorPlan, positionInDirection))
                return false;

            var roomAtPosition = floorPlan.GetPassagesAt(positionInDirection);

            return
                Take(direction)
                    .Map(Invert)
                    .Map(roomAtPosition.Passages.Has);
        }

        private static RoomPassages GetPassagesAt(this FloorPlanInProgress floorPlan, ChunkPosition position)
        {
            return floorPlan.RoomsByChunks[position];
        }

    }

}