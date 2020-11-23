using AChildsCourage.Game.Floors;
using System.Collections.Generic;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal delegate IRNG RNGSource(int seed);

    internal delegate FloorPlanInProgress RoomAdder(FloorPlanInProgress floorPlan);

    internal delegate ChunkPosition ChunkChooser(FloorPlanInProgress floorPlan);

    internal delegate RoomPassages RoomChooser(FloorPlanInProgress floorPlan, ChunkPosition position);

    internal delegate FloorPlan FloorPlanCreator(FloorPlanInProgress floorPlan);

    internal delegate RoomPlan RoomPlanCreator(ChunkPosition position);

    internal delegate RoomPassages RoomPassageLookup(ChunkPosition position);

    internal delegate void RoomPlacer(ChunkPosition position, RoomPassages room);

    internal class FloorPlanInProgress
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