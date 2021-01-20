using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AChildsCourage.Game
{

    public readonly struct TilePositionCollection : IEnumerable<TilePosition>
    {
        
        public static TilePosition Average(TilePositionCollection positions) =>
            new TilePosition((int) positions.Select(p => p.X).Average(),
                             (int) positions.Select(p => p.Y).Average());
        

        private readonly ImmutableHashSet<TilePosition> tilePositions;


        public TilePositionCollection(ImmutableHashSet<TilePosition> tilePositions) =>
            this.tilePositions = tilePositions;
        
        public TilePositionCollection(IEnumerable<TilePosition> tilePositions) =>
            this.tilePositions = tilePositions.ToImmutableHashSet();


        public IEnumerator<TilePosition> GetEnumerator() => tilePositions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}