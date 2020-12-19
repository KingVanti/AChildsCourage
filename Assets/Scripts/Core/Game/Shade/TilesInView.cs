using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
{

    public static class MTilesInView
    {

        public static Func<IEnumerable<TilePosition>, TilesInView> ToTilesInView =>
            positions => new TilesInView(positions);

        public readonly struct TilesInView : IEnumerable<TilePosition>
        {

            private readonly ImmutableHashSet<TilePosition> positions;


            public TilesInView(IEnumerable<TilePosition> positions) => this.positions = ImmutableHashSet.CreateRange(positions);


            public IEnumerator<TilePosition> GetEnumerator() => positions.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        }

    }

}