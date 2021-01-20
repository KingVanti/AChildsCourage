using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    internal readonly struct Aoi
    {

        internal static Aoi ToAoi(IEnumerable<Vector2> positions) =>
            new Aoi(positions.Select(p => p.Map(Poi.ToPoi)).ToImmutableHashSet());


        internal ImmutableHashSet<Poi> Pois { get; }


        private Aoi(ImmutableHashSet<Poi> pois) =>
            Pois = pois;

    }

}