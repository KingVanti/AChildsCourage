using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MChunkPosition;

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

        public readonly struct GenerationParameters
        {

            public RoomPassages[] Passages { get; }


            public GenerationParameters(RoomPassages[] passages) => Passages = passages;

        }

        internal class FloorPlanInProgress
        {

            internal Dictionary<ChunkPosition, RoomPassages> RoomsByChunks { get; } = new Dictionary<ChunkPosition, RoomPassages>();

            internal List<ChunkPosition> ReservedChunks { get; } = new List<ChunkPosition>();

        }


        internal readonly struct RoomInChunk
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