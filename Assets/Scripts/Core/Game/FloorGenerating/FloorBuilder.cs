using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MFloorGenerating.MRoomBuilder;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MFloorBuilder
        {

            public static FloorBuilder EmptyFloorBuilder =>
                new FloorBuilder(ImmutableHashSet<Wall>.Empty,
                                 ImmutableHashSet<RoomBuilder>.Empty);

            public static FloorBuilder PlaceRoom(FloorBuilder floor, RoomBuilder room) =>
                new FloorBuilder(floor.Walls, floor.Rooms.Add(room));

            public static ChunkPosition GetEndRoomChunkPosition(FloorBuilder floorBuilder) =>
                GetEndRoom(floorBuilder).ChunkPosition;

            private static RoomBuilder GetEndRoom(FloorBuilder floorBuilder) =>
                floorBuilder.Rooms.First(r => r.RoomType == RoomType.End);


            public readonly struct FloorBuilder
            {

                public ImmutableHashSet<Wall> Walls { get; }

                public ImmutableHashSet<RoomBuilder> Rooms { get; }

                public FloorBuilder(ImmutableHashSet<Wall> walls, ImmutableHashSet<RoomBuilder> rooms)
                {
                    Walls = walls;
                    Rooms = rooms;
                }

            }

        }

    }

}