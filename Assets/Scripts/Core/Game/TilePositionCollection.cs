using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game
{

    public readonly struct TilePositionCollection : IEnumerable<TilePosition>
    {

        private readonly ImmutableHashSet<TilePosition> tilePositions;


        public TilePositionCollection(IEnumerable<TilePosition> tilePositions) =>
            this.tilePositions = tilePositions.ToImmutableHashSet();


        public IEnumerator<TilePosition> GetEnumerator() => tilePositions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}