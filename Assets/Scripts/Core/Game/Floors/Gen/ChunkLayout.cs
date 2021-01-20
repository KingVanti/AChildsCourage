using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.Game.Chunk;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct ChunkLayout
    {

        public const int BaseChunkCount = 5;


        private static readonly (int, int) noSize = (0, 0);


        private static IEnumerable<Chunk> BasePositions
        {
            get
            {
                yield return new Chunk(0, 0);
                yield return new Chunk(1, 0);
                yield return new Chunk(-1, 0);
                yield return new Chunk(0, 1);
                yield return new Chunk(0, -1);
            }
        }

        private static ChunkLayout EmptyChunkLayout => new ChunkLayout(ImmutableHashSet<Chunk>.Empty);

        public static ChunkLayout BaseChunkLayout =>
            BasePositions.Aggregate(EmptyChunkLayout, OccupyIn);


        public static IEnumerable<Chunk> GetPositions(ChunkLayout layout) =>
            layout.occupiedChunks;

        public static (int Width, int Height) GetDimensions(ChunkLayout layout) =>
            layout.Map(IsEmpty)
                ? noSize
                : layout.occupiedChunks.Map(Chunk.GetDimensions);

        private static bool IsEmpty(ChunkLayout layout) =>
            layout.occupiedChunks.IsEmpty;

        public static int CountDirectConnections(ChunkLayout layout, Chunk position) =>
            GetAdjacentChunks(position)
                .Count(IsOccupiedIn, layout);

        public static int CountIndirectConnections(ChunkLayout layout, Chunk position) =>
            GetDiagonalAdjacentChunks(position)
                .Count(IsOccupiedIn, layout);

        public static ChunkLayout OccupyIn(ChunkLayout layout, Chunk position) =>
            new ChunkLayout(layout.occupiedChunks.Add(position));

        public static IEnumerable<Chunk> GetPossibleNextChunks(ChunkLayout layout)
        {
            bool CanOccupy(Chunk position) =>
                !IsOccupiedIn(layout, position);

            return layout.occupiedChunks
                         .SelectMany(GetAdjacentChunks)
                         .Where(CanOccupy);
        }

        public static bool IsOccupiedIn(ChunkLayout layout, Chunk position) =>
            layout.occupiedChunks.Contains(position);


        private readonly ImmutableHashSet<Chunk> occupiedChunks;


        private ChunkLayout(ImmutableHashSet<Chunk> occupiedChunks) =>
            this.occupiedChunks = occupiedChunks;

    }

}