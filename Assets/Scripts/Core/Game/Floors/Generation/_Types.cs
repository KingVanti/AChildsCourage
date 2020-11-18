
using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        private delegate ChunkPosition ChooseChunk(FloorPlanBuilder builder);

        private delegate RoomPassages ChooseRoom(FloorPlanBuilder builder, ChunkPosition chunkPosition);

        internal class FloorPlanBuilder
        {

            internal Dictionary<ChunkPosition, RoomPassages> RoomsByChunks { get; } = new Dictionary<ChunkPosition, RoomPassages>();

            internal List<ChunkPosition> ReservedChunks { get; } = new List<ChunkPosition>();

        }

        public enum GenerationPhase
        {
            StartRoom,
            NormalRooms,
            EndRoom
        }

    }

}