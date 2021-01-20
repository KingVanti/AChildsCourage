using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.Game.Floors.Gen.ChunkLayout;
using static AChildsCourage.Game.Chunk;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct PassagePlan
    {

        private static PassagePlan Empty => new PassagePlan(ImmutableDictionary<Chunk, ChunkPassages>.Empty);


        public static PassagePlan CreatePassagePlan(ChunkLayout layout)
        {
            bool HasAdjacentChunkIn(PassageDirection direction, Chunk position) =>
                position
                    .Map(GetAdjacentChunk, direction)
                    .Map(IsOccupiedIn, layout);

            ChunkPassages GetPassagesFor(Chunk position) =>
                new ChunkPassages(position.Map(HasAdjacentChunkIn, PassageDirection.North),
                                  position.Map(HasAdjacentChunkIn, PassageDirection.East),
                                  position.Map(HasAdjacentChunkIn, PassageDirection.South),
                                  position.Map(HasAdjacentChunkIn, PassageDirection.West));

            PassagePlan AddPassagesFor(PassagePlan plan, Chunk position) =>
                new PassagePlan(plan.passages.Add(position, GetPassagesFor(position)));

            return layout.Map(ChunkLayout.GetChunks)
                         .Aggregate(Empty, AddPassagesFor);
        }

        public static IEnumerable<Chunk> GetChunks(PassagePlan plan) =>
            plan.passages.Keys;

        public static RoomFilter CreateFilterFor(Chunk position, PassagePlan plan)
        {
            Chunk GetFurthestChunkFromOrigin() =>
                plan.Map(GetChunks)
                    .FirstByDescending(GetDistanceToOrigin);

            var passages = plan.passages[position];

            var roomType =
                position.Equals(OriginChunk) ? RoomType.Start
                : position.Equals(GetFurthestChunkFromOrigin()) ? RoomType.End
                : RoomType.Normal;

            return new RoomFilter(roomType, passages);
        }


        private readonly ImmutableDictionary<Chunk, ChunkPassages> passages;


        private PassagePlan(ImmutableDictionary<Chunk, ChunkPassages> passages)
            => this.passages = passages;

    }

}