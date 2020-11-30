using System.Collections.Generic;
using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorPlanGenerating
    {

        public enum GenerationPhase
        {

            StartRoom,
            NormalRooms,
            EndRoom

        }

        internal delegate FloorPlan GenerateFloorPlan(int seed);

        internal class FloorPlanInProgress
        {

            internal Dictionary<ChunkPosition, RoomPassages> RoomsByChunks { get; } = new Dictionary<ChunkPosition, RoomPassages>();

            internal List<ChunkPosition> ReservedChunks { get; } = new List<ChunkPosition>();

        }


        internal struct RoomInChunk
        {

            internal RoomPassages Room { get; }

            internal ChunkPosition Position { get; }


            internal RoomInChunk(RoomPassages room, ChunkPosition position)
            {
                Room = room;
                Position = position;
            }

        }

    }

}