using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.Game.Chunk;
using static AChildsCourage.Game.ChunkCollection;

namespace AChildsCourage.Game.Floors.Gen
{

    internal readonly struct ChunkLayout
    {

        internal const int BaseChunkCount = 5;


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

        private static ChunkLayout EmptyChunkLayout => new ChunkLayout(EmptyChunkCollection);

        internal static ChunkLayout BaseChunkLayout =>
            BasePositions.Aggregate(EmptyChunkLayout, OccupyIn);


        internal static ChunkCollection GetChunks(ChunkLayout layout) =>
            layout.occupiedChunks;

        internal static (int Width, int Height) GetDimensions(ChunkLayout layout) =>
            layout.Map(IsEmpty)
                ? noSize
                : layout.occupiedChunks
                        .Map(GetBounds)
                        .Map(IntBounds.GetDimensions);

        private static bool IsEmpty(ChunkLayout layout) =>
            layout.occupiedChunks.Map(ChunkCollection.IsEmpty);

        internal static int CountDirectConnections(ChunkLayout layout, Chunk chunk) =>
            GetAdjacentChunks(chunk)
                .Count(IsOccupiedIn, layout);

        internal static int CountIndirectConnections(ChunkLayout layout, Chunk chunk) =>
            GetDiagonalAdjacentChunks(chunk)
                .Count(IsOccupiedIn, layout);

        internal static ChunkLayout OccupyIn(ChunkLayout layout, Chunk chunk) =>
            new ChunkLayout(layout.occupiedChunks.Map(Add, chunk));

        internal static IEnumerable<Chunk> GetPossibleNextChunks(ChunkLayout layout)
        {
            bool CanOccupy(Chunk position) =>
                !IsOccupiedIn(layout, position);

            return layout.occupiedChunks
                         .Select(GetAdjacentChunks)
                         .Map(Combine)
                         .Where(CanOccupy);
        }

        internal static bool IsOccupiedIn(ChunkLayout layout, Chunk position) =>
            layout.occupiedChunks.Contains(position);


        private readonly ChunkCollection occupiedChunks;


        private ChunkLayout(ChunkCollection occupiedChunks) =>
            this.occupiedChunks = occupiedChunks;

    }

}