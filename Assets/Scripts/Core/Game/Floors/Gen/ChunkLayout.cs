using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static AChildsCourage.Game.MChunkPosition;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct ChunkLayout
    {

        public const int BaseChunkCount = 5;


        private static readonly (int, int) noSize = (0, 0);


        private static IEnumerable<ChunkPosition> BasePositions
        {
            get
            {
                yield return new ChunkPosition(0, 0);
                yield return new ChunkPosition(1, 0);
                yield return new ChunkPosition(-1, 0);
                yield return new ChunkPosition(0, 1);
                yield return new ChunkPosition(0, -1);
            }
        }

        private static ChunkLayout EmptyChunkLayout => new ChunkLayout(ImmutableHashSet<ChunkPosition>.Empty);

        public static ChunkLayout BaseChunkLayout =>
            BasePositions.Aggregate(EmptyChunkLayout, OccupyIn);


        public static IEnumerable<ChunkPosition> GetPositions(ChunkLayout layout) =>
            layout.occupiedChunks;

        public static (int Width, int Height) GetDimensions(ChunkLayout layout) =>
            layout.Map(IsEmpty)
                ? noSize
                : layout.occupiedChunks.Map(MChunkPosition.GetDimensions);

        private static bool IsEmpty(ChunkLayout layout) =>
            layout.occupiedChunks.IsEmpty;

        public static int CountDirectConnections(ChunkLayout layout, ChunkPosition position) =>
            GetAdjacentChunks(position)
                .Count(IsOccupiedIn, layout);

        public static int CountIndirectConnections(ChunkLayout layout, ChunkPosition position) =>
            GetDiagonalAdjacentChunks(position)
                .Count(IsOccupiedIn, layout);

        public static ChunkLayout OccupyIn(ChunkLayout layout, ChunkPosition position) =>
            new ChunkLayout(layout.occupiedChunks.Add(position));

        public static IEnumerable<ChunkPosition> GetPossibleNextChunks(ChunkLayout layout)
        {
            bool CanOccupy(ChunkPosition position) =>
                !IsOccupiedIn(layout, position);

            return layout.occupiedChunks
                         .SelectMany(GetAdjacentChunks)
                         .Where(CanOccupy);
        }

        public static bool IsOccupiedIn(ChunkLayout layout, ChunkPosition position) =>
            layout.occupiedChunks.Contains(position);


        private readonly ImmutableHashSet<ChunkPosition> occupiedChunks;


        private ChunkLayout(ImmutableHashSet<ChunkPosition> occupiedChunks) =>
            this.occupiedChunks = occupiedChunks;

    }

}