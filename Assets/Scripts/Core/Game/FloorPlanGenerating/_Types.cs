using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game
{

    public static partial class MFloorPlanGenerating
    {

        public enum GenerationPhase
        {

            StartRoom,
            NormalRooms,
            EndRoom

        }

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