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


        public static ChunkLayout BaseChunkLayout =>
            new ChunkLayout(ImmutableHashSet<ChunkPosition>.Empty)
                .Map(OccupyChunkIn, new ChunkPosition(0, 0))
                .Map(OccupyChunkIn, new ChunkPosition(1, 0))
                .Map(OccupyChunkIn, new ChunkPosition(-1, 0))
                .Map(OccupyChunkIn, new ChunkPosition(0, 1))
                .Map(OccupyChunkIn, new ChunkPosition(0, -1));


        public static IEnumerable<ChunkPosition> Positions(ChunkLayout layout) =>
            layout.occupiedChunks;

        public static (int Width, int Height) GetDimensions(ChunkLayout layout)
        {
            bool IsEmpty() =>
                layout.occupiedChunks.IsEmpty;

            if (IsEmpty()) return noSize;
            return layout.occupiedChunks.Map(MChunkPosition.GetDimensions);
        }

        public static ChunkLayout OccupyChunkIn(ChunkLayout layout, ChunkPosition position) =>
            new ChunkLayout(layout.occupiedChunks.Add(position));

        public static IEnumerable<ChunkPosition> GetPossibleNextChunks(ChunkLayout layout)
        {
            bool IsPossible(ChunkPosition position) =>
                !IsOccupiedIn(layout, position);

            return layout.occupiedChunks
                         .SelectMany(GetAdjacentChunks)
                         .Where(IsPossible);
        }

        public static bool IsOccupiedIn(ChunkLayout layout, ChunkPosition position) =>
            layout.occupiedChunks.Contains(position);


        private readonly ImmutableHashSet<ChunkPosition> occupiedChunks;


        private ChunkLayout(ImmutableHashSet<ChunkPosition> occupiedChunks) =>
            this.occupiedChunks = occupiedChunks;

    }

}