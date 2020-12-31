using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.F;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MOldFloorGenerating.MFloorLayout;

namespace AChildsCourage.Game
{

    public static partial class MOldFloorGenerating
    {

        public static class MFloorLayoutBuilder
        {

            public static FloorLayoutBuilder EmptyFloorLayoutBuilder =>
                new FloorLayoutBuilder(ImmutableList<ChunkPosition>.Empty,
                                       ImmutableHashSet<ChunkPosition>.Empty);

            private static RoomType GetRoomTypeByIndex(FloorLayoutBuilder builder, int roomIndex) =>
                roomIndex == 0 ? RoomType.Start
                : roomIndex == GetRoomCount(builder) - 1 ? RoomType.End
                : RoomType.Normal;

            private static int GetRoomCount(FloorLayoutBuilder builder) =>
                builder.OccupiedChunks.Count;

            internal static bool HasReservedChunks(FloorLayoutBuilder builder) =>
                builder.ReservedChunks.Any();

            public static LayoutGenerationPhase GetCurrentPhase(FloorLayoutBuilder builder, GenerationParameters parameters)
            {
                var roomCount = GetRoomCount(builder);

                return roomCount == 0 ? LayoutGenerationPhase.StartRoom
                    : roomCount == parameters.RoomCount - 1 ? LayoutGenerationPhase.EndRoom
                    : LayoutGenerationPhase.NormalRooms;
            }

            internal static bool IsOccupied(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                builder.OccupiedChunks.Contains(chunk);

            internal static FloorLayoutBuilder AddRoom(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                Take(builder)
                    .Map(OccupyChunk, chunk)
                    .Map(ClearChunkReservation, chunk)
                    .Map(ReserveSurroundingChunks, chunk);

            private static FloorLayoutBuilder OccupyChunk(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                new FloorLayoutBuilder(builder.OccupiedChunks.Add(chunk),
                                       builder.ReservedChunks);

            private static FloorLayoutBuilder ClearChunkReservation(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                new FloorLayoutBuilder(builder.OccupiedChunks,
                                       builder.ReservedChunks.Remove(chunk));

            private static FloorLayoutBuilder ReserveSurroundingChunks(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                Take(chunk)
                    .Map(GetAdjacentChunks)
                    .Where(c => CanReserve(builder, c))
                    .Aggregate(builder, ReserveChunk);

            private static bool CanReserve(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                !HasReserved(builder, chunk) &&
                !IsOccupied(builder, chunk);

            private static bool HasReserved(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                builder.ReservedChunks.Contains(chunk);

            private static FloorLayoutBuilder ReserveChunk(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                new FloorLayoutBuilder(builder.OccupiedChunks,
                                       builder.ReservedChunks.Add(chunk));

            internal static FloorLayout CreateFloorLayout(FloorLayoutBuilder builder) =>
                new FloorLayout(builder.OccupiedChunks
                                       .Select((chunk, index) => new RoomInChunk(chunk, GetRoomTypeByIndex(builder, index)))
                                       .ToImmutableArray());

            public readonly struct FloorLayoutBuilder
            {

                public ImmutableList<ChunkPosition> OccupiedChunks { get; }

                public ImmutableHashSet<ChunkPosition> ReservedChunks { get; }


                public FloorLayoutBuilder(ImmutableList<ChunkPosition> occupiedChunks, ImmutableHashSet<ChunkPosition> reservedChunks)
                {
                    OccupiedChunks = occupiedChunks;
                    ReservedChunks = reservedChunks;
                }

            }

        }

    }

}