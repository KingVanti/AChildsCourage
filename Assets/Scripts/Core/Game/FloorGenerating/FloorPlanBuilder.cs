using System.Collections.Immutable;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MChunkPosition;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MFloorPlanBuilder
        {

            public static FloorPlanBuilder EmptyFloorPlanBuilder => new FloorPlanBuilder(ImmutableDictionary<ChunkPosition, RoomPassages>.Empty);


            public readonly struct FloorPlanBuilder
            {

                public ImmutableDictionary<ChunkPosition, RoomPassages> Rooms { get; }


                public FloorPlanBuilder(ImmutableDictionary<ChunkPosition, RoomPassages> rooms) => Rooms = rooms;

            }

        }

    }

}