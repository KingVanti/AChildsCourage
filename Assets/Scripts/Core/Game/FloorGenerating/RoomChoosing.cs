using System;
using System.Collections.Immutable;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MFloorGenerating.MRoomPassageFiltering;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayout;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MRoomChoosing
        {

            public static Func<RoomInChunk, FloorLayout, ImmutableHashSet<RoomPassages>, CreateRng, RoomPassagesInChunk> ChooseRoom =>
                (room, layout, roomPassages, rng) =>
                    CreateFilter(layout, room)
                        .Map(filter => FilterPassagesMatching(filter, roomPassages))
                        .Map(ChooseRandom, rng)
                        .Map(passages => new RoomPassagesInChunk(passages, room.Chunk));

            private static RoomPassageFilter CreateFilter(FloorLayout layout, RoomInChunk room) => new RoomPassageFilter(room.RoomType, GetPassagesForChunk(layout, room.Chunk));

            private static RoomPassages ChooseRandom(FilteredRoomPassages roomPassages, CreateRng createRng) => roomPassages.GetRandom(createRng);


            public readonly struct RoomPassagesInChunk
            {

                public RoomPassages Passages { get; }

                public ChunkPosition Chunk { get; }


                public RoomPassagesInChunk(RoomPassages passages, ChunkPosition chunk)
                {
                    Passages = passages;
                    Chunk = chunk;
                }

            }

        }

    }

}