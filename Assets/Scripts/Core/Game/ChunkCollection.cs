using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AChildsCourage.Game
{

    public readonly struct ChunkCollection : IEnumerable<Chunk>
    {

        public static ChunkCollection EmptyChunkCollection => new ChunkCollection(ImmutableHashSet<Chunk>.Empty);

        private static (int, int, int, int) EmptyChunkBounds => (0, 0, 0, 0);


        public static (int MinX, int MinY, int MaxX, int MaxY) GetBounds(ChunkCollection chunkCollection) =>
            chunkCollection.Map(IsEmpty)
                ? EmptyChunkBounds
                : (chunkCollection.chunks.Min(p => p.X),
                   chunkCollection.chunks.Min(p => p.Y),
                   chunkCollection.chunks.Max(p => p.X),
                   chunkCollection.chunks.Max(p => p.Y));

        public static (int Width, int Height) GetDimensions(ChunkCollection chunkCollection)
        {
            var bounds = GetBounds(chunkCollection);

            var width = bounds.MaxX - bounds.MinX + 1;
            var height = bounds.MaxY - bounds.MinY + 1;

            return (width, height);
        }

        public static Chunk GetLowerLeft(ChunkCollection chunkCollection) =>
            chunkCollection.Map(GetBounds)
                           .Map(b => new Chunk(b.MinX, b.MinY));

        public static bool IsEmpty(ChunkCollection chunkCollection) =>
            chunkCollection.chunks.IsEmpty;

        public static ChunkCollection Add(Chunk chunk, ChunkCollection chunkCollection) =>
            new ChunkCollection(chunkCollection.chunks.Add(chunk));

        public static ChunkCollection Combine(IEnumerable<ChunkCollection> chunkCollections) =>
            chunkCollections.SelectMany(c => c.chunks)
                            .Map(chunks => new ChunkCollection(chunks));


        private readonly ImmutableHashSet<Chunk> chunks;


        public ChunkCollection(ImmutableHashSet<Chunk> chunks) =>
            this.chunks = chunks;

        public ChunkCollection(IEnumerable<Chunk> chunks) =>
            this.chunks = chunks.ToImmutableHashSet();

        public ChunkCollection(params Chunk[] chunks) =>
            this.chunks = chunks.ToImmutableHashSet();


        public IEnumerator<Chunk> GetEnumerator() => chunks.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}