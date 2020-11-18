using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        internal static FloorPlan ToFloorPlan(this IDictionary<ChunkPosition, RoomPassages> roomsByChunks)
        {
            Func<ChunkPosition, RoomPlan> correspondingRoom = p => p.GetRoomAtPosition(roomsByChunks);

            return
                roomsByChunks
                .GetChunkPositions()
                .Select(correspondingRoom)
                .ConvertToFloorPlan();
        }

        private static IEnumerable<ChunkPosition> GetChunkPositions(this IDictionary<ChunkPosition, RoomPassages> roomsByChunks)
        {
            return roomsByChunks.Keys;
        }

        private static RoomPlan GetRoomAtPosition(this ChunkPosition position, IDictionary<ChunkPosition, RoomPassages> roomsByChunks)
        {
            var passages = roomsByChunks[position];
            var transform = new RoomTransform(position, passages.IsMirrored, passages.RotationCount);

            return new RoomPlan(passages.RoomId, transform);
        }

        private static FloorPlan ConvertToFloorPlan(this IEnumerable<RoomPlan> roomPlans)
        {
            return new FloorPlan(roomPlans.ToArray());
        }

    }
}