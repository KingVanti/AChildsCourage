using AChildsCourage.Game.Floors;
using System.Collections.Generic;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal delegate IRNG RNGSource(int seed);

    internal delegate FloorPlanBuilder RoomAdder(FloorPlanBuilder builder);

    internal delegate ChunkPosition ChunkChooser(FloorPlanBuilder builder);

    internal delegate RoomPassages RoomChooser(FloorPlanBuilder builder, ChunkPosition position);

    internal delegate FloorPlan FloorPlanCreator(FloorPlanBuilder builder);

    internal delegate RoomPlan RoomPlanCreator(ChunkPosition position);

    internal delegate RoomPassages RoomPassageLookup(ChunkPosition position);

    internal delegate void RoomPlacer(ChunkPosition position, RoomPassages room);

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