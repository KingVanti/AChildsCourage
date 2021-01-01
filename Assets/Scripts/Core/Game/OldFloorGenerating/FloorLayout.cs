﻿using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.Floors.MChunkPassages;
using static AChildsCourage.Game.MChunkPosition;

namespace AChildsCourage.Game
{

    public static partial class MOldFloorGenerating
    {

        public static class MFloorLayout
        {

            public static ChunkPassages GetPassagesForChunk(FloorLayout layout, ChunkPosition chunk) =>
                new ChunkPassages(IsOccupied(layout, GetAdjacentChunk(chunk, PassageDirection.North)),
                                  IsOccupied(layout, GetAdjacentChunk(chunk, PassageDirection.East)),
                                  IsOccupied(layout, GetAdjacentChunk(chunk, PassageDirection.South)),
                                  IsOccupied(layout, GetAdjacentChunk(chunk, PassageDirection.West)));

            private static bool IsOccupied(FloorLayout layout, ChunkPosition position) =>
                layout.Rooms.Any(r => r.Chunk.Equals(position));

            public readonly struct FloorLayout
            {

                public ImmutableArray<RoomInChunk> Rooms { get; }


                public FloorLayout(ImmutableArray<RoomInChunk> rooms) => Rooms = rooms;

            }

            public readonly struct RoomInChunk
            {

                public ChunkPosition Chunk { get; }

                public RoomType RoomType { get; }


                public RoomInChunk(ChunkPosition chunk, RoomType roomType)
                {
                    Chunk = chunk;
                    RoomType = roomType;
                }

            }

        }

    }

}