using System;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.F;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayout;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MFloorLayoutBuilder
        {

            public static FloorLayoutBuilder EmptyFloorLayoutBuilder =>
                new FloorLayoutBuilder(ImmutableList<ChunkPosition>.Empty,
                                       ImmutableHashSet<ChunkPosition>.Empty);

            private static Func<FloorLayoutBuilder, int> GetRoomCount =>
                builder =>
                    builder.OccupiedChunks.Count;

            internal static Func<FloorLayoutBuilder, bool> HasReservedChunks =>
                builder =>
                    builder.ReservedChunks.Any();

            public static Func<FloorLayoutBuilder, GenerationParameters, LayoutGenerationPhase> GetCurrentPhase =>
                (builder, parameters) =>
                {
                    var roomCount = GetRoomCount(builder);

                    return roomCount == 0 ? LayoutGenerationPhase.StartRoom
                        : roomCount == parameters.RoomCount - 1 ? LayoutGenerationPhase.EndRoom
                        : LayoutGenerationPhase.NormalRooms;
                };

            internal static Func<FloorLayoutBuilder, ChunkPosition, bool> IsOccupied =>
                (builder, chunk) =>
                    builder.OccupiedChunks.Contains(chunk);

            internal static Func<FloorLayoutBuilder, ChunkPosition, FloorLayoutBuilder> AddRoom =>
                (builder, chunk) =>
                    Take(builder)
                        .MapWith(OccupyChunk, chunk)
                        .MapWith(ClearChunkReservation, chunk)
                        .MapWith(ReserveSurroundingChunks, chunk);

            private static Func<FloorLayoutBuilder, ChunkPosition, FloorLayoutBuilder> OccupyChunk =>
                (builder, chunk) =>
                    new FloorLayoutBuilder(
                                           builder.OccupiedChunks.Add(chunk),
                                           builder.ReservedChunks);

            private static Func<FloorLayoutBuilder, ChunkPosition, FloorLayoutBuilder> ClearChunkReservation =>
                (builder, chunk) =>
                    new FloorLayoutBuilder(
                                           builder.OccupiedChunks,
                                           builder.ReservedChunks.Remove(chunk));

            private static Func<FloorLayoutBuilder, ChunkPosition, FloorLayoutBuilder> ReserveSurroundingChunks =>
                (builder, chunk) =>
                    Take(chunk)
                        .Map(GetAdjacentChunks)
                        .Where(c => CanReserve(builder, c))
                        .Aggregate(builder, ReserveChunk);

            private static Func<FloorLayoutBuilder, ChunkPosition, bool> CanReserve =>
                (builder, position) =>
                    !HasReserved(builder, position) &&
                    !IsOccupied(builder, position);

            private static Func<FloorLayoutBuilder, ChunkPosition, bool> HasReserved =>
                (builder, position) =>
                    builder.ReservedChunks.Contains(position);

            private static Func<FloorLayoutBuilder, ChunkPosition, FloorLayoutBuilder> ReserveChunk =>
                (builder, chunk) =>
                    new FloorLayoutBuilder(
                                           builder.OccupiedChunks,
                                           builder.ReservedChunks.Add(chunk));

            internal static Func<FloorLayoutBuilder, FloorLayout> CreateFloorLayout =>
                builder =>
                    new FloorLayout(
                                    builder.OccupiedChunks
                                           .Select((chunk, index) => new RoomInChunk(chunk, GetRoomTypeByIndex(builder, index)))
                                           .ToImmutableArray());

            private static Func<FloorLayoutBuilder, int, RoomType> GetRoomTypeByIndex =>
                (builder, roomIndex) =>
                    roomIndex == 0 ? RoomType.Start
                    : roomIndex == GetRoomCount(builder) - 1 ? RoomType.End
                    : RoomType.Normal;

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